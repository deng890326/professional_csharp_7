using Microsoft.EntityFrameworkCore;

namespace Intro
{
    public class BooksContext : DbContext
    {
        public BooksContext(DbContextOptions<BooksContext> options)
            : base(options) { }

        public DbSet<Book> Books { get; set; }
    }
}
