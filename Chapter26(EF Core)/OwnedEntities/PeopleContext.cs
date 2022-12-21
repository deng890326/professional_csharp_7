using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OwnedEntities
{
    internal class PeopleContext : DbContext
    {
        private const string connectionString =
            @"server=(localdb)\MSSQLLocalDB;database=People;" +
            @"trusted_connection=true";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Debug);
        }

        public DbSet<Person> People { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>(buildePerson);
        }

        private void buildePerson(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(p => p.PersonId);
            builder.OwnsOne<Address>(p => p.CompanyAddress)
                .OwnsOne<Location>(a => a.Location);
            builder.OwnsOne<Address>(p => p.PrivateAddress)
                .ToTable("Addr")
                .OwnsOne<Location>(a => a.Location);
        }
    }
}
