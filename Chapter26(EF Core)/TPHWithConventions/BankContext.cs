using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPHWithConventions
{
    internal class BankContext : DbContext
    {
        private const string connectionString =
            @"server=(localdb)\MSSQLLocalDB;database=LocalBank;" +
            @"trusted_connection=true";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Debug);
        }

        public DbSet<Payment> Payments { get; protected set; }
        public DbSet<CreditcardPayment> CreditcardPayments { get; protected set; }
        public DbSet<CashPayment> CashPayments { get; protected set; }
    }
}
