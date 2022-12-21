using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Payment>(buildPayment);
        }

        private void buildPayment(EntityTypeBuilder<Payment> builder)
        {
            //const string PaymentType = nameof(PaymentType);
            //const int Cash = 1;
            //const int Creditcard = 2;

            builder.HasKey(P => P.PaymentId);
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Amount).HasColumnType("money");
            //builder.HasDiscriminator<int>(PaymentType)
            //    .HasValue<CashPayment>(Cash)
            //    .HasValue<CreditcardPayment>(Creditcard);
            builder.HasDiscriminator<PaymentType>(nameof(PaymentType))
                .HasValue<CashPayment>(PaymentType.Cash)
                .HasValue<CreditcardPayment>(PaymentType.Creditcard);
        }

        private enum PaymentType
        {
            Cash = 1,
            Creditcard = 2,
        }
    }
}
