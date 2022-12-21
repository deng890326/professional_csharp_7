using Microsoft.Extensions.Configuration;
using System.Collections;
using System.Data.SqlClient;

namespace ConnectionSamples
{
    internal class Program
    {
        const string TABLE_NAME = "ProCSharp.Books";
        const string ID = "Id";
        const string TITLE = "Title";
        const string PUBLISHER = "Publisher";
        const string ISBN = "Isbn";
        const string RELEASE_DATE = "ReleaseDate";

        static void Main(string[] args)
        {
            try
            {
                using var connection = OpenConnection();

                ExecuteScalarSample(connection);
                Console.WriteLine();

                InsertSample(connection);
                Console.WriteLine();

                ExecuteReaderSample(connection, "%C#%");
                Console.WriteLine();

                StoredProcedure(connection, "Wrox Press");
                Console.WriteLine();

                TransactionSample(connection).Wait();
                Console.WriteLine();

                ShowStatistics(connection.RetrieveStatistics());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static void ExecuteScalarSample(SqlConnection connection)
        {
            const string TITLE_FILTER = "Professional C#%";
            using SqlCommand command = connection.CreateCommand();
            command.CommandText = $"SELECT {TITLE} FROM {TABLE_NAME}" +
                $" WHERE lower({TITLE}) LIKE @{TITLE}";
            command.Parameters.AddWithValue(TITLE, TITLE_FILTER);
            object s = command.ExecuteScalar();
            Console.WriteLine(s);
        }

        private static void InsertSample(SqlConnection connection)
        {
            const string SQL = $"INSERT INTO {TABLE_NAME}" +
                $" ({TITLE}, {PUBLISHER}, {ISBN}, {RELEASE_DATE})" +
                $" VALUES (@{TITLE}, @{PUBLISHER}, @{ISBN}, @{RELEASE_DATE})";
            using SqlCommand sqlCommand = new(SQL, connection);
            sqlCommand.Parameters.AddWithValue(TITLE, "Professional C# 7 and .NET Core 2.0");
            sqlCommand.Parameters.AddWithValue(PUBLISHER, "Wrox Press");
            sqlCommand.Parameters.AddWithValue(ISBN, "978-1119449270");
            sqlCommand.Parameters.AddWithValue(RELEASE_DATE, new DateTime(2018, 4, 2));
            int records = sqlCommand.ExecuteNonQuery();
            Console.WriteLine($"{records} records inserted.");
        }

        private static void ExecuteReaderSample(SqlConnection connection, string titleFilter)
        {
            const string SQL = $"SELECT {ID}, {TITLE}, {PUBLISHER}, {RELEASE_DATE}" +
                $" FROM {TABLE_NAME} WHERE lower({TITLE}) LIKE @{TITLE}" +
                $" ORDER BY {RELEASE_DATE} DESC";
            SqlCommand command = new(SQL, connection);
            command.Parameters.AddWithValue(TITLE, titleFilter);
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string title = reader.GetString(1);
                string publisher = reader.GetString(2);
                DateTime? releaseDate = reader.IsDBNull(3) ?
                    null : reader.GetDateTime(3);
                Console.WriteLine($"{id,5}. {title,-40} {publisher,-15} {releaseDate:d}");
            }
        }

        private static void StoredProcedure(SqlConnection connection, string publisher)
        {

            using SqlCommand command = connection.CreateCommand();
            command.CommandText = "ProCSharp.GetBooksByPublisher";
            command.CommandType = System.Data.CommandType.StoredProcedure;
            SqlParameter param = command.CreateParameter();
            param.ParameterName = "@publisher";
            param.SqlDbType = System.Data.SqlDbType.NVarChar;
            param.Value = publisher;
            command.Parameters.Add(param);
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                int id = (int)reader[ID];
                string title = (string)reader[TITLE];
                DateTime? releaseDate = (DateTime?)reader[RELEASE_DATE];
                Console.WriteLine($"{id,5}. {title,-40} {publisher,-15} {releaseDate:d}");
            }
        }

        private static async Task TransactionSample(SqlConnection connection)
        {
            const string SQL = $"INSERT INTO {TABLE_NAME} " +
                $"({TITLE}, {PUBLISHER}, {ISBN}, {RELEASE_DATE}) " +
                $"VALUES (@{TITLE}, @{PUBLISHER}, @{ISBN}, @{RELEASE_DATE}); " +
                "SELECT SCOPE_IDENTITY()";

            using SqlTransaction transaction = (SqlTransaction)
                await connection.BeginTransactionAsync();
            using SqlCommand command = connection.CreateCommand();
            command.CommandText = SQL;
            command.Transaction = transaction;
            command.Parameters.AddWithValue(TITLE, "Professional C# 8 and .NET Core 3.0");
            command.Parameters.AddWithValue(PUBLISHER, "Wrox Press");
            command.Parameters.AddWithValue(ISBN, "42-08154711");
            command.Parameters.AddWithValue(RELEASE_DATE, new DateTime(2020, 9, 2));

            object? id = await command.ExecuteScalarAsync();
            Console.WriteLine($"record added with id: {id}");

            await transaction.CommitAsync();
        }

        static SqlConnection OpenConnection()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json")
                .Build();

            var conn = new SqlConnection(config["Data:DefaultConnection:Connection"]);
            conn.InfoMessage += Conn_InfoMessage;
            conn.StateChange += Conn_StateChange;
            conn.StatisticsEnabled = true;
            conn.FireInfoMessageEventOnUserErrors = true;
            conn.Open();

            return conn;
        }

        static void ShowStatistics(IDictionary statistics)
        {
            Console.WriteLine("Statistics");
            foreach (var key in statistics.Keys)
            {
                Console.WriteLine($"{key}: {statistics[key]}");
            }
            Console.WriteLine();
        }

        private static void Conn_StateChange(object sender, System.Data.StateChangeEventArgs e)
        {
            Console.WriteLine($"{nameof(Conn_StateChange)}: {e.OriginalState} -> {e.CurrentState}");
        }

        private static void Conn_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            Console.WriteLine($"{nameof(Conn_InfoMessage)}: {e.Message}");
        }
    }
}