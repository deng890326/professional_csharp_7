using BooksLib.Models;
using BooksLib.Services;
using Framewok;
using Framewok.Services;
using Framewok.ViewModels;
using System.ComponentModel;
using System.Threading.Tasks;

namespace BooksLib.ViewModels
{
    public class BookMaterDetailViewModel : MaterDetailViewModel<BookViewModel, Book>
    {
        public BookMaterDetailViewModel(IItemsService<Book> service, INavigationService navigationService)
            : base(service)
        {
            _navigationService = navigationService;
            GoToDetailCmd = new RelayCommand(NavigateToDetailPageAsync, () => SelectedItem != null);

        }

        public RelayCommand GoToDetailCmd;
        private void NavigateToDetailPageAsync() =>
            _navigationService.NavigateToAsync(PageNames.BookDetailPage);

        protected override void OnAdd()
        {
            Book newBook = new Book();
            Items.Add(newBook);
            SelectedItem = newBook;
            NavigateToDetailPageAsync();
        }

        protected override BookViewModel ToViewModel(Book item)
        {
            return new BookViewModel(_service, item);
        }

        private readonly INavigationService _navigationService;
    }
}
