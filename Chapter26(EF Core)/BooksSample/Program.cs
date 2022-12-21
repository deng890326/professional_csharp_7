using Microsoft.EntityFrameworkCore;

namespace BooksSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            AddBooksAsync().Wait();
            Console.WriteLine();
            UseEFFunctions("C#").Wait();
            Console.WriteLine();
            UseNormalFunctions("C#").Wait();
        }

        static async Task AddBooksAsync()
        {
            Console.WriteLine($"{nameof(AddBooksAsync)}: begin");
            using var context = new BooksContext();

            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();

            Console.WriteLine($"{nameof(AddBooksAsync)}: begin AddBook");
            Book b1 = new("Professional C# 6 and .NET Core 1.0", "Wrox Press");
            Book b2 = new("Professional C# 7 and .NET Core 2.0", "Wrox Press");
            await context.Books.AddRangeAsync(b1, b2);
            int records = await context.SaveChangesAsync();
            Console.WriteLine($"{nameof(AddBooksAsync)}: {records} book(s) added.");

            Console.WriteLine($"{nameof(AddBooksAsync)}: begin Remove b1");
            context.Books.Remove(b1);
            records = await context.SaveChangesAsync();
            Console.WriteLine($"{nameof(AddBooksAsync)}: {records} book(s) removed.");
        }

        static async Task UseEFFunctions(string titleSegment)
        {
            Console.WriteLine($"{nameof(UseEFFunctions)}: begin");
            using var context = new BooksContext();
            string likeExpr = $"%{titleSegment}%";
            IQueryable<Book> query = 
                from book in context.Books
                where EF.Functions.Like(book.Title, likeExpr)
                select book;
            var books = await query.ToListAsync();
            foreach (Book book in books) {
                Console.WriteLine($"{nameof(UseEFFunctions)}: {book.Title} {book.Publisher}");
            }
        }

        static async Task UseNormalFunctions(string titleSegment)
        {
            Console.WriteLine($"{nameof(UseNormalFunctions)}: begin");
            using var context = new BooksContext();
            IQueryable<Book> query =
                from book in context.Books
                where book.Title.Contains(titleSegment)
                select book;
            var books = await query.ToListAsync();
            foreach (Book book in books)
            {
                Console.WriteLine($"{nameof(UseNormalFunctions)}: {book.Title} {book.Publisher}");
            }
        }
    }
}