using Framewok.ViewModels;

namespace BooksAppX.Views
{
    internal interface IView<TViewModel>
        where TViewModel : ViewModelBase
    {
        TViewModel ViewModel { get; }
    }
}
