using Microsoft.EntityFrameworkCore;

namespace RelationUsingAnnotations
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using var context = new BooksContext();
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();

            await AddBooksAsync(context);
            Console.WriteLine();

            await ExplicitLoading(context);
            //await EagerLoading(context);
            Console.WriteLine();

            Console.ReadKey();
        }

        private static async Task EagerLoading(BooksContext context)
        {
            Console.WriteLine($"{nameof(EagerLoading)}: begin.");
            Book? book = await
                (from b in context.Books
                 .Include(b => b.Chapters)
                 .Include(b => b.Author)
                 .Include(b => b.Reviewer)
                 .Include(b => b.ProjectEditor)
                 where EF.Functions.Like(b.Title, "%C#%")
                 select b).FirstOrDefaultAsync();
            if (book != null)
            {
                Console.WriteLine($"title: {book.Title}");
                var abs = book.Author?.AuthoredBooks ?? Enumerable.Empty<Book>();
                Console.WriteLine($"author: {book.Author}, authored books: {string.Join(", ", abs)}");
                var rbs = book.Reviewer?.ReviewedBooks ?? Enumerable.Empty<Book>();
                Console.WriteLine($"reviewer: {book.Reviewer}, reviewed books: {string.Join(", ", rbs)}");
                var ebs = book.ProjectEditor?.EditedBooks ?? Enumerable.Empty<Book>();
                Console.WriteLine($"project editer: {book.ProjectEditor}, edited books: {string.Join(", ", ebs)}");
                foreach (var chapter in book.Chapters)
                {
                    Console.WriteLine("chapters:");
                    Console.WriteLine($"{chapter.Book} {chapter.Number}. {chapter.Title}");
                }
            }
        }

        private static async Task ExplicitLoading(BooksContext context)
        {
            Console.WriteLine($"{nameof(ExplicitLoading)}: begin.");
            Book? book = await
                (from b in context.Books
                 where b.Title.StartsWith("Professional C#")
                 select b).FirstOrDefaultAsync();
            if (book != null)
            {
                Console.WriteLine($"title: {book.Title}");
                await context.Entry(book).Collection(b => b.Chapters).LoadAsync();
                await context.Entry(book).Reference(b => b.Author).LoadAsync();
                await context.Entry(book).Reference(b => b.Reviewer).LoadAsync();
                await context.Entry(book).Reference(b => b.ProjectEditor).LoadAsync();
                var abs = book.Author?.AuthoredBooks ?? Enumerable.Empty<Book>();
                Console.WriteLine($"author: {book.Author}, AuthoredBooks: {string.Join(", ", abs)}");
                var rbs = book.Reviewer?.ReviewedBooks ?? Enumerable.Empty<Book>();
                Console.WriteLine($"reviewer: {book.Reviewer}, reviewed books: {string.Join(", ", rbs)}");
                var ebs = book.ProjectEditor?.EditedBooks ?? Enumerable.Empty<Book>();
                Console.WriteLine($"project editer: {book.ProjectEditor}, edited books: {string.Join(", ", ebs)}");
                foreach (var chapter in book.Chapters)
                {
                    Console.WriteLine("chapters:");
                    Console.WriteLine($"{chapter.Book} {chapter.Number}. {chapter.Title}");
                }
            }

        }

        private static async Task AddBooksAsync(BooksContext context)
        {
            Console.WriteLine($"{nameof(AddBooksAsync)}: begin.");
            Book b1 = new Book("Professional C# 6 and .NET Core 1.0")
            {
                Author = "Deng Yingwei",
                Reviewer = "Chen Lu",
                ProjectEditor = "Xiao ming"
            };
            b1.Chapters.Add(new Chapter() { Number = 1, Title = "C# Introduction" });
            b1.Chapters.Add(new Chapter() { Number = 2, Title = "C# Core" });
            await context.Books.AddAsync(b1);
            int records = await context.SaveChangesAsync();
            Console.WriteLine($"{nameof(AddBooksAsync)} {records} records added");
        }


    }
}