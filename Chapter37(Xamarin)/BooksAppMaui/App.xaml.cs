namespace BooksAppMaui
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        public static new App? Current => (App?)Application.Current;

        public AppShell? AppShell => (AppShell?)MainPage;
    }
}