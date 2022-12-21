using DISampleLib;
using System;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace UWPClient
{
    internal class MessageService : IMessageService
    {
        public async Task<IUICommand> ShowMessageAsync(string message)
        {
            return await new MessageDialog(message).ShowAsync();
        }

        Task IMessageService.ShowMessageAsync(string message) => ShowMessageAsync(message);
    }
}