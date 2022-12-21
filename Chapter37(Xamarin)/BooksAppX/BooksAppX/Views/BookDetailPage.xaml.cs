using BooksLib.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BooksAppX.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookDetailPage : ContentPage, IView<BookDetailViewModel>
    {
        public BookDetailPage()
        {
            InitializeComponent();
            ViewModel = AppServices.GetRequired<BookDetailViewModel>();
            BindingContext = ViewModel;
        }

        public BookDetailViewModel ViewModel { get; }
    }
}