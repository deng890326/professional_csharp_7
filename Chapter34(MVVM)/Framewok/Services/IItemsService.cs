using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Framewok.Services
{
    public interface IItemsService<TItem>
        where TItem : BindableBase
    {
        ObservableCollection<TItem> Items { get; }

        TItem? SelectedItem { get; set; }

        event EventHandler SelectedItemChanged;

        Task<bool> RefreshAsync();

        Task<TItem?> AddOrUpdateAsync(TItem item);

        Task<bool> RemoveAsync(TItem item);
    }
}
