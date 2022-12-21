using Microsoft.EntityFrameworkCore;

namespace TableSplitting
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using var context = new MenusContext();
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();

            await AddSampleDataAsync(context);
            Console.WriteLine();

            await QuerySampleAsync(context);
            Console.WriteLine();

            await UpdateSampleDataAsync(context);
            Console.WriteLine();

            await UpdateSampleData2Async(context);
            Console.WriteLine();

            Console.ReadKey();
        }

        private static async Task QuerySampleAsync(MenusContext context)
        {
            const string TAG = nameof(QuerySampleAsync);
            Console.WriteLine($"{TAG}: begin.");
            await foreach (var menu in context.Menus)
            {
                Print(TAG, menu);
            }
        }

        private static void Print(string prefix, Menu? menu)
        {
            if (menu == null)
            {
                Console.WriteLine($"{prefix}: menu is null");
                return;
            }
            Console.WriteLine($"{prefix}: {menu.MenuId}, {menu.Title}, {menu.SubTitle}, " +
                $"{menu.Price}, {menu.Details?.MenuDetailsId}, " +
                $"{menu.Details?.KitchenInfo}, {menu.Details?.MenusSold}");
            Console.WriteLine($"{prefix}: menu.MenuId == menu.Details?.MenuDetailsId ? " +
                $"{menu.MenuId == menu.Details?.MenuDetailsId}");
        }

        private static async Task UpdateSampleDataAsync(MenusContext context)
        {
            const string TAG = nameof(UpdateSampleDataAsync);
            Console.WriteLine($"{TAG}: begin.");
            Menu? menu = await context.Menus.FirstOrDefaultAsync();
            Console.WriteLine($"{TAG}: before update:");
            Print(TAG, menu);
            if (menu != null)
            {
                int menuId = menu.MenuId;
                var oldDetails = menu.Details;
                menu.Details = new MenuDetails()
                {
                    KitchenInfo = oldDetails.KitchenInfo + " Updated",
                    MenusSold = ++oldDetails.MenusSold
                };
                int records = await context.SaveChangesAsync();
                Console.WriteLine($"{TAG}: {records} record(s) saved");

                Menu? menu1 = await context.Menus.FindAsync(menuId);
                Console.WriteLine($"{TAG}: After update:");
                Print(TAG, menu1);
            }
        }

        private static async Task UpdateSampleData2Async(MenusContext context)
        {
            const string TAG = nameof(UpdateSampleData2Async);
            Console.WriteLine($"{TAG}: begin.");
            MenuDetails? menuDetails = await context.MenuDetails.FirstOrDefaultAsync();
            Console.WriteLine($"{TAG}: before update:");
            Print(TAG, menuDetails?.Menu);
            if (menuDetails != null)
            {
                int menuId = menuDetails.Menu.MenuId;
                var oldMenu = menuDetails.Menu;
                menuDetails.Menu = new Menu()
                {
                    Title = oldMenu.Title + " Updated",
                    SubTitle = oldMenu.SubTitle + " Updated",
                    Price = ++oldMenu.Price,
                };
                try
                {
                    int records = await context.SaveChangesAsync();
                    Console.WriteLine($"{TAG}: {records} record(s) saved");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{nameof(Exception)}: {ex.Message}");
                }

                Menu? menu1 = await context.Menus.FindAsync(menuId);
                Console.WriteLine($"{TAG}: After update:");
                Print(TAG, menu1);
            }
        }

        private static async Task AddSampleDataAsync(MenusContext context)
        {
            const string TAG = nameof(AddSampleDataAsync);
            Console.WriteLine($"{TAG}: begin.");
            context.Menus.Add(new Menu()
            {
                Title = "Test",
                SubTitle = "Test Sub",
                Price = 10,
                Details = new MenuDetails()
                {
                    KitchenInfo = "Beautiful",
                    MenusSold = 100
                }
            });
            int records = await context.SaveChangesAsync();
            Console.WriteLine($"{TAG}: {records} record(s) saved.");
        }
    }
}