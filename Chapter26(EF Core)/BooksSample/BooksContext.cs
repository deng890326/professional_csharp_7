using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BooksSample.ColumnNames;

namespace BooksSample
{
    internal class BooksContext : DbContext
    {
        private const string connectionString =
            @"server=(localdb)\MSSQLLocalDB;database=WroxBooks;" +
            @"trusted_connection=true";

        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(connectionString)
                .LogTo(Console.WriteLine, LogLevel.Trace);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>(builder =>
            {
                // 使用约定的字段_title
                builder.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(50);

                // 显式给出字段名
                builder.Property(b => b.Publisher)
                .HasField("_publisher")
                .IsRequired(false)
                .HasMaxLength(30);

                // 只有字段的列
                builder.Property<int>("_bookId")
                .HasColumnName(BookId)
                .IsRequired();

                builder.HasKey("_bookId");

                // 阴影属性
                builder.Property<bool>(IsDeleted);
                builder.Property<DateTime>(LastUpdated);

                // 全局过滤器
                builder.HasQueryFilter(b => EF.Property<bool>(b, IsDeleted) == false);
            });
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            processShawdowProperties();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            processShawdowProperties();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        // 处理阴影属性IsDeleted、LastUpdated
        private void processShawdowProperties()
        {
            ChangeTracker.DetectChanges();

            foreach (var item in from e in ChangeTracker.Entries<Book>()
                                 where (e.State == EntityState.Added
                                    || e.State == EntityState.Modified
                                    || e.State == EntityState.Deleted)
                                 select e)
            {
                item.CurrentValues[LastUpdated] = DateTime.Now;

                if (item.State == EntityState.Deleted)
                {
                    item.State = EntityState.Modified;
                    item.CurrentValues[IsDeleted] = true;
                }
            }
        }
    }
}
