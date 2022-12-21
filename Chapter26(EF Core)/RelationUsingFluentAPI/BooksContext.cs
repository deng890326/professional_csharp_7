using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationUsingFluentAPI
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
            modelBuilder.Entity<Book>(buildBookModel);
            modelBuilder.Entity<Chapter>(buildChapterModel);
            modelBuilder.Entity<User>(buildUserModel);
        }

        private void buildBookModel(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.BookId);
            builder.HasMany(b => b.Chapters)
                .WithOne(c => c.Book);
            builder.HasOne(b => b.Author)
                .WithMany(u => u.AuthoredBooks)
                .HasForeignKey("AuthorId");
            builder.HasOne(b => b.Reviewer)
                .WithMany(u => u.ReviewedBooks)
                .HasForeignKey("ReviewerId");
            builder.HasOne(b => b.ProjectEditor)
                .WithMany(u => u.EditedBooks)
                .HasForeignKey("EditorId");
        }

        private void buildChapterModel(EntityTypeBuilder<Chapter> builder)
        {
            builder.HasKey(c => c.ChapterId);
            builder.HasOne(c => c.Book)
                .WithMany(b => b.Chapters)
                .HasForeignKey(c => c.BookId);
        }

        private void buildUserModel(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.UserId);
            builder.HasMany(u => u.AuthoredBooks)
                .WithOne(b => b.Author);
            builder.HasMany(u => u.ReviewedBooks)
                .WithOne(b => b.Reviewer);
            builder.HasMany(u => u.EditedBooks)
                .WithOne(b => b.ProjectEditor);
        }
    }
}
