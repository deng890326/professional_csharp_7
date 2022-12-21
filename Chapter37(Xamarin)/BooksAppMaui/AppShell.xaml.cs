namespace BooksAppMaui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        public void BooksTabAddPage(Page page)
        {
            ShellContent shellContent = new()
            {
                Title = page.Title,
                Content = page
            };
            BooksTab.Items.Add(shellContent);
        }
    }
}