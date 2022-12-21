using BooksAppX.Views;
using BooksLib.Services;
using Rg.Plugins.Popup.Services;
using System.Threading.Tasks;

namespace BooksAppX
{
    internal class AppMessageService : IMessageService
    {
        public Task ShowMessageAsync(string title, string message)
        {
            return PopupNavigation.Instance.PushAsync(new MessagePopup(title, message));
        }
    }
}