using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;

namespace Intro
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Program p = new Program();
            p.AddLogging();

            await p.CreateTheDataBaseAsync();
            Console.WriteLine();

            await p.AddBookAsync();
            Console.WriteLine();

            await p.AddBooksAsync();
            Console.WriteLine();

            Console.WriteLine("ReadBooksAsync");
            await p.ReadBooksAsync();
            Console.WriteLine();

            Console.WriteLine("QueryBooks");
            p.QueryBooks();
            Console.WriteLine();

            await p.UpdateBookAsync();
            Console.WriteLine();

            Console.WriteLine("ReadBooksAsync");
            await p.ReadBooksAsync();
            Console.WriteLine();

            Console.WriteLine("QueryBooks");
            p.QueryBooks();
            Console.WriteLine();

            await p.DeleteBooksAsync();
            Console.WriteLine();

            Console.ReadKey();
        }

        private async Task CreateTheDataBaseAsync()
        {
            using var context = new BooksContext();
            bool created = await context.Database.EnsureCreatedAsync();
            string creationInfo = created ? "created" : "exists";
            Console.WriteLine($"database {creationInfo}");
        }

        private async Task AddBookAsync()
        {
            Book book = new Book()
            {
                Title = "Sample Book",
                Publisher = "Wrox Press"
            };
            using var context = new BooksContext();
            await context.Books.AddAsync(book);
            int records = await context.SaveChangesAsync();
            Console.WriteLine($"{records} book(s) added.");
        }

        private async Task AddBooksAsync()
        {
            using var context = new BooksContext();
            Book b1 = new Book()
            {
                Title = "Professional C# 6 and .NET Core 1.0",
                Publisher = "Wrox Press"
            };
            Book b2 = new Book()
            {
                Title = "Professional C# 7 and .NET Core 2.0",
                Publisher = "Wrox Press"
            };
            await context.Books.AddRangeAsync(b1, b2);
            int records = await context.SaveChangesAsync();
            Console.WriteLine($"{records} book(s) added.");
        }

        private async Task ReadBooksAsync()
        {
            using var context = new BooksContext();
            List<Book> books = await context.Books.ToListAsync();
            foreach (Book book in books)
            {
                Console.WriteLine($"{book.Title} {book.Publisher}");
            }
        }

        private void QueryBooks()
        {
            using var context = new BooksContext();
            IQueryable<Book> books =
                from book in context.Books
                where book.Publisher == "Wrox Press"
                select book;
            foreach (Book b in books)
            {
                Console.WriteLine($"{b.Title} {b.Publisher}");
            }
        }

        private async Task UpdateBookAsync()
        {
            using var context = new BooksContext();
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
            Console.WriteLine($"{records} book(s) updated.");
        }

        private async Task DeleteBooksAsync()
        {
            using var context = new BooksContext();
            context.Books.RemoveRange(context.Books);
            int records = await context.SaveChangesAsync();
            Console.WriteLine($"{records} book(s) deleted");
        }

        private void AddLogging()
        {
            using var context = new BooksContext();
            var provider = context.GetInfrastructure<IServiceProvider>();
            var loggerFactory = provider.GetService<ILoggerFactory>();
            loggerFactory.AddProvider(new ConsoleLoggerProvider(
                new OptionsMonitor<ConsoleLoggerOptions>(
                    new OptionsFactory<ConsoleLoggerOptions>(
                        Array.Empty<IConfigureOptions<ConsoleLoggerOptions>>(),
                        Array.Empty<IPostConfigureOptions<ConsoleLoggerOptions>>()
                        ),
                    Array.Empty<IOptionsChangeTokenSource<ConsoleLoggerOptions>>(),
                    new OptionsCache<ConsoleLoggerOptions>()
                    )
                )
            );
        }
    }
}