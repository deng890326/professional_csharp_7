using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableSplitting
{
    internal class MenusContext : DbContext
    {
        private const string connectionString =
            @"server=(localdb)\MSSQLLocalDB;database=Menus;" +
            @"trusted_connection=true";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Debug);
        }

        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuDetails> MenuDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Menu>(buildMenu);
            modelBuilder.Entity<MenuDetails>(buildMenuDetails);
        }

        private void buildMenu(EntityTypeBuilder<Menu> builder)
        {
            builder.HasKey(m => m.MenuId);
            builder.HasOne(m => m.Details)
                .WithOne(d => d.Menu)
                .HasForeignKey<MenuDetails>(d => d.MenuDetailsId);
            builder.ToTable(nameof(Menus));
        }

        private void buildMenuDetails(EntityTypeBuilder<MenuDetails> builder)
        {
            builder.HasKey(d => d.MenuDetailsId);
            //builder.HasOne(d => d.Menu)
            //    .WithOne(m => m.Details);
                //.HasForeignKey<Menu>(m => m.MenuId);
            builder.ToTable(nameof(Menus));
        }
    }
}
