using BooksLib.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BooksAppX.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BooksPage : ContentPage, IView<BookMaterDetailViewModel>
    {
        public BooksPage()
        {
            InitializeComponent();
            ViewModel = AppServices.GetRequired<BookMaterDetailViewModel>();
            BindingContext = ViewModel;
        }

        public BookMaterDetailViewModel ViewModel { get; }

        private void MyListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ViewModel.GoToDetailCmd.Execute(sender);
        }
    }
}
