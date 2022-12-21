using Framewok.Services;

namespace Framewok.ViewModels
{
    public class ItemViewModel<TItem> : ViewModelBase, IItemViewModel<TItem>
        where TItem : BindableBase
    {
        public ItemViewModel(IItemsService<TItem> itemsService, TItem item)
        {
            ItemsService = itemsService;
            _item = item;
        }

        public virtual TItem Item
        {
            get => _item;
            set => Set(ref _item, value);
        }
        
        protected IItemsService<TItem> ItemsService { get; }

        private TItem _item;
    }

}
