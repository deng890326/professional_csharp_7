using Intro;
using Microsoft.EntityFrameworkCore;

namespace BooksSampleUsingDI
{
    internal class BooksService
    {
        public BooksService(BooksContext context)
        {
            this.context = context;
        }

        public async Task CreateTheDataBaseAsync()
        {
            Console.WriteLine($"{nameof(CreateTheDataBaseAsync)}: begin.");
            bool created = await context.Database.EnsureCreatedAsync();
            string creationInfo = created ? "created" : "exists";
            Console.WriteLine($"{nameof(CreateTheDataBaseAsync)}: database {creationInfo}");
        }

        public async Task DeleteTheDataBaseAsync()
        {
            Console.WriteLine($"{nameof(DeleteTheDataBaseAsync)}: begin.");
            bool deleted = await context.Database.EnsureDeletedAsync();
            string deletionInfo = deleted ? "deleted" : "not exists";
            Console.WriteLine($"{nameof(DeleteTheDataBaseAsync)}: database {deletionInfo}");
        }

        public async Task AddBookAsync()
        {
            Console.WriteLine($"{nameof(AddBookAsync)}: begin.");
            Book book = new()
            {
                Title = "Sample Book",
                Publisher = "Wrox Press"
            };
            await context.Books.AddAsync(book);
            int records = await context.SaveChangesAsync();
            Console.WriteLine($"{nameof(AddBookAsync)}: {records} book(s) added.");
        }

        public async Task AddBooksAsync()
        {
            Console.WriteLine($"{nameof(AddBooksAsync)}: begin.");
            Book b1 = new()
            {
                Title = "Professional C# 6 and .NET Core 1.0",
                Publisher = "Wrox Press"
            };
            Book b2 = new()
            {
                Title = "Professional C# 7 and .NET Core 2.0",
                Publisher = "Wrox Press"
            };
            await context.Books.AddRangeAsync(b1, b2);
            int records = await context.SaveChangesAsync();
            Console.WriteLine($"{nameof(AddBooksAsync)}: {records} book(s) added.");
        }

        public async Task ReadBooksAsync()
        {
            Console.WriteLine($"{nameof(ReadBooksAsync)}: begin.");
            List<Book> books = await context.Books.ToListAsync();
            foreach (Book book in books)
            {
                Console.WriteLine($"{nameof(ReadBooksAsync)}: {book.Title} {book.Publisher}");
            }
        }

        public async Task QueryBooksAsync()
        {
            Console.WriteLine($"{nameof(QueryBooksAsync)}: begin.");
            IQueryable<Book> books =
                from book in context.Books
                where book.Publisher == "Wrox Press"
                select book;
            await foreach (Book b in books.AsAsyncEnumerable())
            {
                Console.WriteLine($"{nameof(QueryBooksAsync)}: {b.Title} {b.Publisher}");
            }
        }

        public async Task UpdateBookAsync()
        {
            Console.WriteLine($"{nameof(UpdateBookAsync)}: begin.");
            var query = from b in context.Books
                        where b.Title == "Sample Book"
                        select b;
            Book? book = await query.FirstOrDefaultAsync();
            int records = 0;
            if (book != null)
            {
                book.Title = "Web Design with HTML and CSS";
                book.Publisher = "For Dummies";
                records = await context.SaveChangesAsync();
            }
            Console.WriteLine($"{nameof(UpdateBookAsync)}: {records} book(s) updated.");
        }

        public async Task DeleteBooksAsync()
        {
            Console.WriteLine($"{nameof(DeleteBooksAsync)}: begin.");
            context.Books.RemoveRange(context.Books);
            int records = await context.SaveChangesAsync();
            Console.WriteLine($"{nameof(DeleteBooksAsync)}: {records} book(s) deleted");
        }

        private readonly BooksContext context;
    }
}
