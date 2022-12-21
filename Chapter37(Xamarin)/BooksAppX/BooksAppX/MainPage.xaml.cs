using Xamarin.Forms;

namespace BooksAppX
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public new INavigation Navigation => NavigationPage.Navigation;
    }
}
