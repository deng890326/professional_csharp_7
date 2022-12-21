using BooksAppX.Views;
using BooksLib.Services;
using Framewok.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BooksAppX
{
    internal class AppNavigationService : INavigationService
    {
        public string CurrentPage => throw new NotImplementedException();

        public Task NavigateBackAsnc() =>
            Navigation.PopAsync();

        public Task NavigateToAsync(string page) =>
            Navigation.PushAsync(_pages[page]());

        public bool UseNavigation() => true;

        internal static AppNavigationService Create(IServiceProvider _) => _instance;

        private static readonly AppNavigationService _instance;

        static AppNavigationService() =>
            _instance = new AppNavigationService();

        private AppNavigationService() { }

        private readonly Dictionary<string, Func<Page>> _pages =
            new Dictionary<string, Func<Page>>()
            {
                [PageNames.BooksPage] = () => new BooksPage(),
                [PageNames.BookDetailPage] = () => new BookDetailPage()
            };

        private INavigation Navigation
        {
            get
            {
                MainPage mainPage = (MainPage)Application.Current.MainPage;
                return mainPage.Navigation;
            }
        }
    }
}