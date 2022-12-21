using Intro;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BooksSampleUsingDI
{
    internal class Program
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public const string PROGRAM_LOG = "program";

        private const string connectionString =
            @"server=(localdb)\MSSQLLocalDB;database=WroxBooks;" +
            @"trusted_connection=true";

        static Program()
        {
            IServiceCollection services = new ServiceCollection()
                .AddDbContext<BooksContext>(ConfigBooksServiceOptions)
                .AddTransient<BooksService>();
                //.AddLogging(ConfigureLogging);
            ServiceProvider = services.BuildServiceProvider();
        }

        private static void ConfigureLogging(ILoggingBuilder builder)
        {
        }

        private static void ConfigBooksServiceOptions(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(connectionString)
                .EnableSensitiveDataLogging()
                .LogTo(Console.WriteLine, LogLevel.Trace);
                //.LogTo(Console.WriteLine, (eId, l) => true);
        }

        static async Task Main(string[] args)
        {
            BooksService booksService = ServiceProvider.GetRequiredService<BooksService>();

            await booksService.DeleteTheDataBaseAsync();
            Console.WriteLine();

            await booksService.CreateTheDataBaseAsync();
            Console.WriteLine();

            await booksService.AddBookAsync();
            Console.WriteLine();

            await booksService.AddBooksAsync();
            Console.WriteLine();

            await booksService.ReadBooksAsync();
            Console.WriteLine();

            await booksService.QueryBooksAsync();
            Console.WriteLine();

            await booksService.UpdateBookAsync();
            Console.WriteLine();

            await booksService.ReadBooksAsync();
            Console.WriteLine();

            await booksService.QueryBooksAsync();
            Console.WriteLine();

            await booksService.DeleteBooksAsync();
            Console.WriteLine();

            Console.ReadKey();
        }
    }
}