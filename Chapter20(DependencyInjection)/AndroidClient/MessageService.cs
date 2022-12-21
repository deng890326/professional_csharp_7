using DISampleLib;

namespace AndroidClient
{
    internal class MessageService : IMessageService
    {
        public Task ShowMessageAsync(string message)
        {
            new AlertDialog.Builder(App.CurrentActivity).SetMessage(message)?.Show();
            return Task.CompletedTask;
        }
    }
}