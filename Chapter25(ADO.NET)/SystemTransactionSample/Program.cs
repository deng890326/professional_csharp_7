using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data.Common;
using System.Text;
using System.Transactions;

namespace SystemTransactionSample
{
    internal class Program
    {
        public static ServiceProvider ServiceProvider { get; private set; }

        static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection()
                .AddSingleton(GetConfigurationRoot)
                .AddTransient(GetSqlConnection)
                .AddSingleton<BookData>();
            ServiceProvider = services.BuildServiceProvider();

            //CommittableTransactionAsync().Wait();
            //DependentTransaction();
            //ScopedTransaction();
            NestedScopes();
        }

        private static async Task CommittableTransactionAsync()
        {
            using CommittableTransaction transaction = new();
            Utilities.DisplayTxInfo("Tx Created", transaction.TransactionInformation);

            try
            {
                var b = new Book()
                {
                    Title = "A Dog in The House",
                    Publisher = "Pet Show",
                    Isbn = RandomIsbn(),
                    ReleaseDate = new DateTime(2018, 11, 24)
                };
                var bookData = ServiceProvider.GetRequiredService<BookData>();
                int ret = await bookData.AddBookAsync(b, transaction);
                if (Utilities.AbortTx())
                {
                    throw new ApplicationException("transaction aborted by the user.");
                }
                await Task.Factory.FromAsync(transaction.BeginCommit, transaction.EndCommit, null);
                Console.WriteLine($"transaction completed, ret={ret}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine();
                transaction.Rollback();
            }

            Utilities.DisplayTxInfo("Tx completed", transaction.TransactionInformation);
        }

        private static void ScopedTransaction()
        {
            using var scope = new TransactionScope();
            AmbientTransaction("ambient transaction");
            scope.Complete();
        }

        private static void NestedScopes()
        {
            using var outter = new TransactionScope();
            AmbientTransaction("outter transaction");
            using var inner = new TransactionScope(TransactionScopeOption.Required);
            AmbientTransaction("inner transaction");
            inner.Complete();
            outter.Complete();
        }

        private static void AmbientTransaction(string transationName)
        {
            Transaction.Current.TransactionCompleted += (_, e) =>
            {
                Utilities.DisplayTxInfo($"{transationName} completed",
                    e.Transaction.TransactionInformation);
                Console.WriteLine();
            };
            Utilities.DisplayTxInfo($"{transationName} created",
                Transaction.Current.TransactionInformation);
            Console.WriteLine();

            var b = new Book()
            {
                Title = "Cats in The House",
                Publisher = "Pet Show",
                Isbn = RandomIsbn(),
                ReleaseDate = new DateTime(2019, 11, 24)
            };

            try
            {
                var bookData = ServiceProvider.GetRequiredService<BookData>();
                var addBookTask = bookData.AddBookAsync(b);
                addBookTask.Wait();
                Console.WriteLine($"addBookTask.Result={addBookTask.Result}");
                if (Utilities.AbortTx())
                {
                    throw new ApplicationException($"{transationName} aborted by the user.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine();
                Transaction.Current.Rollback();
            }
        }

        private static void DependentTransaction()
        {
            static async Task UsingDenpentTransactionAsync(DependentTransaction transaction)
            {
                transaction.TransactionCompleted += static (_, e) =>
                {
                    Utilities.DisplayTxInfo("Dependent transaction completed",
                        e.Transaction?.TransactionInformation);
                    Console.WriteLine();
                };

                try
                {
                    await Task.Delay(2000);

                    transaction.Complete();
                    Utilities.DisplayTxInfo("Dependent transaction send complete",
                        transaction.TransactionInformation);
                    Console.WriteLine();
                }
                finally
                {
                    transaction.Dispose();
                }
            }

            using CommittableTransaction transaction = new();
            Utilities.DisplayTxInfo("Root transaction created.",
                transaction.TransactionInformation);
            Console.WriteLine();

            transaction.TransactionCompleted += static (_, e) =>
            {
                Utilities.DisplayTxInfo("Root transaction completed",
                    e.Transaction?.TransactionInformation);
                Console.WriteLine();
            };

            try
            {
                DependentTransaction denpendent = transaction.DependentClone(DependentCloneOption.BlockCommitUntilComplete);
                var dependentTask = UsingDenpentTransactionAsync(denpendent);

                if (Utilities.AbortTx())
                {
                    throw new ApplicationException("transaction aborted by the user.");
                }

                transaction.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine();
                transaction.Rollback();
            }
        }

        private static string RandomIsbn()
        {
            Random random = new Random();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 2; i++)
            {
                sb.Append(random.Next(10));
            }
            sb.Append('-');
            for (int i = 0; i < 8; i++)
            {
                sb.Append(random.Next(10));
            }
            return sb.ToString();
        }

        static IConfigurationRoot GetConfigurationRoot(IServiceProvider _)
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json")
                .Build();
        }

        static SqlConnection GetSqlConnection(IServiceProvider provider)
        {
            var config = provider.GetRequiredService<IConfigurationRoot>();
            var conn = new SqlConnection(config["Data:DefaultConnection:Connection"]);
            conn.InfoMessage += static (_, e) =>
            {
                Console.WriteLine($"{nameof(conn.InfoMessage)}:  {e.Message}");
            };
            conn.StateChange += static (_, e) =>
            {
                Console.WriteLine($"{nameof(conn.StateChange)}: {e.OriginalState} -> {e.CurrentState}");
            };
            conn.StatisticsEnabled = true;
            conn.FireInfoMessageEventOnUserErrors = true;

            return conn;
        }
    }
}