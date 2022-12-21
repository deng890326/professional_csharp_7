using MenuPlanner.Models;
using Microsoft.EntityFrameworkCore;

namespace MenuPlanner.Services
{
    public class MenuCardsService : IMenuCardsService
    {
        public MenuCardsService(MenuCardsContext context)
        {
            _context = context;
        }

        public async Task AddMenuAsync(Menu menu)
        {
            await EnsureDatabaseCreatedAsnyc();
            await _context.Menus.AddAsync(menu);
            await _context.SaveChangesAsync();
        }

        public async Task<Menu?> GetMenuByIdAsync(int id)
        {
            await EnsureDatabaseCreatedAsnyc();
            return await _context.Menus.Include(m =>m.MenuCard).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<MenuCard>> GetMenuCardsAsync()
        {
            await EnsureDatabaseCreatedAsnyc();
            return await _context.MenuCards.ToListAsync();
        }

        public async Task<IEnumerable<Menu>> GetMenusAsync()
        {
            await EnsureDatabaseCreatedAsnyc();
            return await _context.Menus.Include(m => m.MenuCard).ToListAsync();
        }

        public async Task RemoveMenuAsync(int id)
        {
            await EnsureDatabaseCreatedAsnyc();
            Menu? menu = await GetMenuByIdAsync(id);
            if (menu != null)
            {
                _context.Remove(menu);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateMenuAsync(Menu menu)
        {
            await EnsureDatabaseCreatedAsnyc();
            _context.Menus.Update(menu);
            await _context.SaveChangesAsync();
        }

        private Task EnsureDatabaseCreatedAsnyc() =>
            new MenuCardDatabaseInitializer(_context).CreateAndSeedDatabaseAsync();

        private MenuCardsContext _context;
    }
}
