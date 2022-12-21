using Xamarin.Forms;

namespace BooksAppX
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            AppServices.Init();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
