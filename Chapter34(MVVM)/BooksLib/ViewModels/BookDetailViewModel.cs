using BooksLib.Models;
using BooksLib.Services;
using Framewok.Services;
using Framewok.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BooksLib.ViewModels
{
    public class BookDetailViewModel : EditableItemViewModel<Book>
    {
        public BookDetailViewModel(IItemsService<Book> itemsService,
                                   ILogger<BookDetailViewModel> logger,
                                   INavigationService navigationService,
                                   IMessageService messageService)
            : base(itemsService, new Book())
        {
            Logger = logger;
            NavigationService = navigationService;
            MessageService = messageService;
            EditEvent += BookDetailViewModel_EditEventAsync;
            if (Item.BookId == 0)
            {
                BeginEdit();
            }
            itemsService.SelectedItemChanged += ItemService_SelectedItemChanged;
        }

        public override Book Item
        {
            get => ItemsService.SelectedItem ?? base.Item;
            set
            {
                ItemsService.SelectedItem = value;
                RaisePropertyChanged();
            }
        }

        private void ItemService_SelectedItemChanged(object sender, EventArgs e)
        {
            RaisePropertyChanged(nameof(Item));
        }

        private async void BookDetailViewModel_EditEventAsync(object? sender, EditEventArgs e)
        {
            if (e.EditState == EditState.End)
            {
                await MessageService.ShowMessageAsync(e.IsSuccess ? "Info" : "Error",
                                                e.Message);
                if (e.IsSuccess)
                {
                    await NavigationService.NavigateBackAsnc();
                }
            }
        }

        protected override ILogger Logger { get; }
        private INavigationService NavigationService { get; }
        private IMessageService MessageService { get; }
    }
}