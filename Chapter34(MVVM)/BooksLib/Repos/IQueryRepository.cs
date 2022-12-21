using Framewok;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksLib.Repos
{
    public interface IQueryRepository<TItem, in TKey>
        where TItem : BindableBase
    {
        Task<TItem?> GetItemAsync(TKey id);

        Task<IEnumerable<TItem>> GetItemsAsync();
    }
}
