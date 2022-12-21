using Microsoft.EntityFrameworkCore;

namespace MenuPlanner.Models
{
    public class MenuCardDatabaseInitializer
    {
        public MenuCardDatabaseInitializer(MenuCardsContext context) =>
            _context = context;

        public async Task CreateAndSeedDatabaseAsync()
        {
            if (!s_databaseChecked)
            {
                s_databaseChecked = true;
                await _context.Database.MigrateAsync();
                if (await _context.MenuCards.AnyAsync() == false)
                {
                    _context.MenuCards.Add(
                        new MenuCard() { Name = "Breadfast", Active = true, Order = 1 });
                    _context.MenuCards.Add(
                        new MenuCard() { Name = "Vegetarian", Active = true, Order = 2 });
                    _context.MenuCards.Add(
                        new MenuCard() { Name = "Steaks", Active =true, Order = 3 });
                    await _context.SaveChangesAsync();
                }
            }
        }

        private static bool s_databaseChecked = false;
        private readonly MenuCardsContext _context;
    }
}
