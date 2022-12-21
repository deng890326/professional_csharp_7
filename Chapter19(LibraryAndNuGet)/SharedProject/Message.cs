#if WINDOWS_UWP
using System;
using System.Threading.Tasks;
using Windows.UI.Popups;
#endif

namespace SharedProject
{
    internal class Message
    {
#if NETCOREAPP2_0_OR_GREATER
        public static void Show(string message) =>
            Console.WriteLine(message);
#elif WINDOWS_UWP
        public static async Task<IUICommand> ShowAsync(string message) =>
            await new MessageDialog(message).ShowAsync();
#endif
        public static int Add(int x, int y) => x + y;
    }
}