using BooksLib.Models;
using Framewok;
using Framewok.Services;
using Framewok.ViewModels;

namespace BooksLib.ViewModels
{
    public class BookViewModel : ItemViewModel<Book>
    {
        public BookViewModel(IItemsService<Book> booksService, Book item)
            : base(booksService, item)
        {
            DeleteCommand = new RelayCommand(Delete);
        }

        public RelayCommand DeleteCommand { get; }

        private async void Delete()
        {
            using var progress = StartInProgress();
            await ItemsService.RemoveAsync(Item);
            //await ItemsService.RefreshAsync();
        }
    }
}