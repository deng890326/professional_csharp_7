using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationUsingConventions
{
    internal class BooksContext : DbContext
    {
        private const string connectionString =
            @"server=(localdb)\MSSQLLocalDB;database=WroxBooks;" +
            @"trusted_connection=true";

        public DbSet<Book> Books { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(connectionString)
                .LogTo(Console.WriteLine, LogLevel.Trace);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
