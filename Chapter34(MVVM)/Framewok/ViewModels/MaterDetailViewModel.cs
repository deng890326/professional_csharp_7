using Framewok.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Framewok.ViewModels
{
    public abstract class MaterDetailViewModel<TItemViewModel, TItem> : ViewModelBase
        where TItemViewModel : ItemViewModel<TItem>
        where TItem : BindableBase
    {
        protected MaterDetailViewModel(IItemsService<TItem> service)
        {
            _service = service ??
                throw Exceptions.Null(nameof(service));
            SetupItems();
            //SetupSeletedItem();
            AddCommand = new RelayCommand(OnAdd);
            RefreshCommand = new RelayCommand(OnRefreshAsync);
            service.SelectedItemChanged += Service_SelectedItemChanged;
        }



        public RelayCommand AddCommand { get; }

        public RelayCommand RefreshCommand { get; }

        public ObservableCollection<TItem> Items => _service.Items;

        public IEnumerable<TItemViewModel> ItemViewModels =>
            Items.Select(it => ToViewModel(it));

        private void SetupItems()
        {
            Items.CollectionChanged += (sender, args) =>
            {
                RaisePropertyChanged(nameof(ItemViewModels));
            };
        }

        public TItem? SelectedItem
        {
            get => _service.SelectedItem;
            set => _service.SelectedItem = value;
        }

        public TItemViewModel? SelectedItemViewModel
        {
            get => SelectedItem != null ? ToViewModel(SelectedItem) : default;
            set => _service.SelectedItem = value?.Item;
        }

        private void Service_SelectedItemChanged(object sender, System.EventArgs e)
        {
            RaisePropertyChanged(nameof(SelectedItem));
            RaisePropertyChanged(nameof(SelectedItemViewModel));
        }

        protected abstract TItemViewModel ToViewModel(TItem item);

        protected abstract void OnAdd();
        private async void OnRefreshAsync()
        {
            using var progress = StartInProgress();
            await _service.RefreshAsync();
        }

        protected readonly IItemsService<TItem> _service;
    }
}
