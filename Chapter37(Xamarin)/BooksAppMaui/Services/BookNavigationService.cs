using BooksAppMaui.Pages;
using Framewok.Services;
using BooksLib.Services;

namespace BooksAppMaui.Services
{
    internal class BookNavigationService : INavigationService
    {
        public string CurrentPage
        {
            get
            {
                Page? currentPage = App.Current?.AppShell?.CurrentPage;
                if (currentPage != null)
                {
                    return _pageNames[currentPage.GetType()];
                }
                return "";
            }
        }

        public Task NavigateBackAsnc()
        {
            return Navigation?.PopAsync() ?? Task.CompletedTask;
        }

        public Task NavigateToAsync(string pageName)
        {
            if (_pages.TryGetValue(pageName, out var pageGetter))
            {
                Page page = pageGetter();
                return Navigation?.PushAsync(page)
                    ?? Task.CompletedTask;
                //AppShell? shell = App.Current?.AppShell;
                //shell?.BooksTabAddPage(page);
            }
            return Task.CompletedTask;
        }

        public bool UseNavigation() => true;

        private INavigation? Navigation
        {
            get
            {
                AppShell? shell = App.Current?.AppShell;
                return shell?.Navigation;
            }
        }

        private Dictionary<string, Func<Page>> _pages = new()
        {
            [PageNames.BooksPage] = MauiProgram.GetRequiredService<BooksPage>,
            [PageNames.BookDetailPage] = MauiProgram.GetRequiredService<BookDetailPage>,
        };
        private Dictionary<Type, string> _pageNames = new()
        {
            [typeof(BooksPage)] = PageNames.BooksPage,
            [typeof(BookDetailPage)] = PageNames.BookDetailPage,
        };
    }
}