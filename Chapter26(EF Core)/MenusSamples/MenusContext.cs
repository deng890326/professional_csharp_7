using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenusSamples
{
    internal class MenusContext : DbContext
    {
        public MenusContext(DbContextOptions<MenusContext> options)
            : base(options) { }

        public DbSet<Menu> Menus { get; set; }
        public DbSet<MenuCard> MenuCards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("mc");
            modelBuilder.ApplyConfiguration(new MenuCardConfiguration());
            modelBuilder.ApplyConfiguration(new MenuConfiguration());
        }
    }
}
