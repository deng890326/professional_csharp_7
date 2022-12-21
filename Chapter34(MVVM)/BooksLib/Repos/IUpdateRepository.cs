using Framewok;
using System.Threading.Tasks;

namespace BooksLib.Repos
{
    public interface IUpdateRepository<TItem, in TKey>
        where TItem : BindableBase
    {
        Task<TItem?> UpdateAsync(TItem item);

        Task<TItem?> AddAsync(TItem item);

        Task<bool> DeleteAsync(TKey id);
    }
}
