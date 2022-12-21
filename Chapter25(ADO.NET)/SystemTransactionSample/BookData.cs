using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace SystemTransactionSample
{
    internal class BookData
    {
        private const string TABLE_NAME = "ProCSharp.Books";
        private const string ID = "Id";
        private const string TITLE = "Title";
        private const string PUBLISHER = "Publisher";
        private const string ISBN = "Isbn";
        private const string RELEASE_DATE = "ReleaseDate";

        public async Task<int> AddBookAsync(Book book, Transaction? transaction = null)
        {
            const string SQL = $"INSERT INTO {TABLE_NAME} " +
                $"({TITLE}, {PUBLISHER}, {ISBN}, {RELEASE_DATE}) " +
                $"VALUES (@{TITLE}, @{PUBLISHER}, @{ISBN}, @{RELEASE_DATE})";

            using var connection = Program.ServiceProvider.GetRequiredService<SqlConnection>();
            await connection.OpenAsync();
            if (transaction != null)
            {
                connection.EnlistTransaction(transaction);
            }
            using SqlCommand command = new SqlCommand(SQL, connection);
            command.Parameters.AddWithValue(TITLE, book.Title);
            command.Parameters.AddWithValue(PUBLISHER, book.Publisher);
            command.Parameters.AddWithValue(ISBN, book.Isbn);
            command.Parameters.AddWithValue(RELEASE_DATE, book.ReleaseDate);
            return await command.ExecuteNonQueryAsync();
        }
    }
}
