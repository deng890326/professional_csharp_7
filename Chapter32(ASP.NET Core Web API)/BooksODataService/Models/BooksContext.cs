using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BooksODataService.Models
{
    public class BooksContext : DbContext
    {
        public BooksContext(DbContextOptions<BooksContext> options)
            : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<BookChapter> Chapters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(BuildBook);
            modelBuilder.Entity<BookChapter>(BuildBookChapter);
        }

        private void BuildBookChapter(EntityTypeBuilder<BookChapter> builder)
        {
            builder.ToTable($"{nameof(BookChapter)}s");
            builder.HasOne(c => c.Book)
                .WithMany(b => b.Chapters)
                .HasForeignKey(c => c.BookId);
        }

        private void BuildBook(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable($"{nameof(Book)}s");
            builder.Property(b => b.Title)
                .HasMaxLength(120)
                .IsRequired();
            builder.Property(b => b.Publisher)
                .HasMaxLength(40)
                .IsRequired(false);
            builder.Property(b => b.Isbn)
                .HasMaxLength(20)
                .IsRequired(false);
            builder.HasMany(b => b.Chapters)
                .WithOne(c => c.Book)
                .HasForeignKey(c => c.BookId);
        }
    }
}
