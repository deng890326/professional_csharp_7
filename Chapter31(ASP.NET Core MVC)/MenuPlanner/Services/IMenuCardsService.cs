using MenuPlanner.Models;

namespace MenuPlanner.Services
{
    public interface IMenuCardsService
    {
        Task AddMenuAsync(Menu menu);
        Task RemoveMenuAsync(int id);
        Task<Menu?> GetMenuByIdAsync(int id);
        Task<IEnumerable<Menu>> GetMenusAsync();
        Task<IEnumerable<MenuCard>> GetMenuCardsAsync();
        Task UpdateMenuAsync(Menu menu);
    }
}
