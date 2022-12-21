using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MenusSamples
{
    internal class Program
    {
        /// <summary>
        /// The ServiceProvider
        /// </summary>
        public static IServiceProvider SP { get; private set; }

        public const string PROGRAM_LOG = "program";

        static Program()
        {
            IServiceCollection services = new ServiceCollection()
                .AddDbContext<MenusContext>(ConfigMenusServiceOptions, ServiceLifetime.Transient)
                .AddTransient<MenusService>();
            SP = services.BuildServiceProvider();
        }

        private static void ConfigMenusServiceOptions(DbContextOptionsBuilder builder)
        {
            const string CONNECTION =
                @"server=(localdb)\MSSQLLocalDB;" +
                "database=MenuCards;" +
                "trusted_connection=true";

            builder.UseSqlServer(CONNECTION)
                .EnableSensitiveDataLogging()
                .LogTo(Console.WriteLine, LogLevel.Error);
        }

        static async Task Main(string[] args)
        {
            using (var scope = SP.CreateAsyncScope())
            {
                var context = SP.GetRequiredService<MenusContext>();
                await context.Database.EnsureDeletedAsync();
                await context.Database.EnsureCreatedAsync();
            }

            using (var scope = SP.CreateAsyncScope())
            {
                var context = SP.GetRequiredService<MenusContext>();
                await AddRecordsAsync(context);
                Console.WriteLine();
            }

            using (var scope = SP.CreateAsyncScope())
            {
                var context = SP.GetRequiredService<MenusContext>();
                await ObjectTrackingAsync(context);
                Console.WriteLine();
            }

            using (var scope = SP.CreateAsyncScope())
            {
                var context = SP.GetRequiredService<MenusContext>();
                await UpdateRecordAsync(context);
                Console.WriteLine();
            }

            await ChangeUntrackedAsync();

            Console.ReadKey();
        }

        private static async Task ChangeUntrackedAsync()
        {
            const string TAG = nameof(ChangeUntrackedAsync);
            Console.WriteLine($"{TAG}: begin.");
            static async Task<Menu> getMenu()
            {
                using var scope = SP.CreateAsyncScope();
                var context = SP.GetRequiredService<MenusContext>();
                return await context.Menus.FirstAsync();
            }
            Menu m = await getMenu();
            m.Price += 0.2m;
            await UpdateUntrackedAsync(m);
        }

        private static async Task UpdateUntrackedAsync(Menu m)
        {
            const string TAG = nameof(UpdateUntrackedAsync);
            Console.WriteLine($"{TAG}: begin.");
            using var scope = SP.CreateAsyncScope();
            var context = SP.GetRequiredService<MenusContext>();
            Console.WriteLine($"{TAG}: before update");
            ShowState(TAG, context);
            //context.Update(m);
            context.Attach(m).State = EntityState.Modified;
            Console.WriteLine($"{TAG}: after update");
            ShowState(TAG, context);
            int records = await context.SaveChangesAsync();
            Console.WriteLine($"{TAG}: {records} record(s) saved.");
            Console.WriteLine($"{TAG}: after save:");
            ShowState(TAG, context);
        }

        private static async Task UpdateRecordAsync(MenusContext context)
        {
            const string TAG = nameof(UpdateRecordAsync);
            Console.WriteLine($"{TAG}: begin.");
            Menu? m = await context.Menus.FirstOrDefaultAsync();
            Console.WriteLine($"{TAG}: before modify");
            ShowState(TAG, context);
            if (m != null) m.Price += 0.2m;
            Console.WriteLine($"{TAG}: after modify");
            ShowState(TAG, context);
            int records = await context.SaveChangesAsync();
            Console.WriteLine($"{TAG}: {records} record(s) saved.");
            ShowState(TAG, context);
        }

        private static async Task ObjectTrackingAsync(MenusContext context)
        {
            const string TAG = nameof(ObjectTrackingAsync);
            Console.WriteLine($"{TAG}: begin.");
            Menu? m1 = await
                (from m in context.Menus
                 where m.Text.StartsWith("Con")
                 select m).FirstOrDefaultAsync();
            Menu? m2 = await
                (from m in context.Menus
                 where m.Text.Contains("(")
                 select m).FirstOrDefaultAsync();
            if (ReferenceEquals(m1, m2))
            {
                Console.WriteLine($"{TAG}: the same object.");
            }
            else
            {
                Console.WriteLine($"{TAG}: not the same object.");
            }
            ShowState(TAG, context);
        }

        private static async Task AddRecordsAsync(MenusContext context)
        {
            const string TAG = nameof(AddRecordsAsync);
            Console.WriteLine($"{TAG}: begin.");
            MenuCard soupCard = new();
            Menu[] soups = new Menu[]
            {
                new Menu
                {
                    MenuCard = soupCard,
                    Text = "Consomme Celestine (with shredded pancake)",
                    Price = 1.0m
                }
            };
            soupCard.Title = "Soups";
            soupCard.Menus.AddRange(soups);
            //context.Attach(soupCard);
            context.Add(soupCard);
            //context.MenuCards.Add(soupCard);
            ShowState(TAG, context);
            int records = await context.SaveChangesAsync();
            Console.WriteLine($"{TAG}: {records} record(s) saved.");
        }


        private static void ShowState(string tag, MenusContext context)
        {
            if (context.ChangeTracker.Entries().Count() == 0)
            {
                Console.WriteLine($"{tag}: no entries");
                return;
            }
            foreach (var entry in context.ChangeTracker.Entries())
            {
                Console.WriteLine($"{tag}: type: {entry.Entity.GetType()}," +
                    $"state: {entry.State}, {entry.Entity} ");
            }
        }
    }
}