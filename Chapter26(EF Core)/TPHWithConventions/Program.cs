using Microsoft.EntityFrameworkCore;

namespace TPHWithConventions
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using var context = new BankContext();
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();

            await AddSampleDataAsync(context);
            Console.WriteLine();

            await AddSampleData2Async(context);
            Console.WriteLine();

            await QuerySample(context);
            Console.WriteLine();

            Console.ReadKey();
        }

        private static async Task AddSampleDataAsync(BankContext context)
        {
            Console.WriteLine($"{nameof(AddSampleDataAsync)}: begin.");
            context.CashPayments.Add(new CashPayment("Donald")
            { Amount = 1 });
            context.CashPayments.Add(new CashPayment("Scrooge")
            { Amount = 2 });
            context.CreditcardPayments.Add(new CreditcardPayment("Gus Goose")
            { Amount = 3, CreditcardNumber = "23456" });

            int record = await context.SaveChangesAsync();
            Console.WriteLine($"{nameof(AddSampleDataAsync)} {record} records saved");
        }

        private static async Task AddSampleData2Async(BankContext context)
        {
            Console.WriteLine($"{nameof(AddSampleData2Async)}: begin.");
            context.Payments.AddRange(
                new CashPayment("Deng") { Amount = 4 },
                new CreditcardPayment("Chen")
                { Amount = 5, CreditcardNumber = "12345" });
            int record = await context.SaveChangesAsync();
            Console.WriteLine($"{nameof(AddSampleData2Async)} {record} records saved");
        }

        private static async Task QuerySample(BankContext context)
        {
            Console.WriteLine($"{nameof(QuerySample)}: begin.");
            var creditcardPayments = context.Payments
                .OfType<CreditcardPayment>()
                .AsAsyncEnumerable();
            await foreach (CreditcardPayment payment in creditcardPayments)
            {
                Console.WriteLine($"{payment.Name} {payment.Amount}");
            }
        }
    }
}