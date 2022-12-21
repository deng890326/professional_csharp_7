#if ANDROID
using Android.App;
using Android.Content;
using Android.Widget;
#endif
using BooksLib.Services;
using Microsoft.Maui.Platform;

namespace BooksAppMaui.Services
{
    internal class AppMessageService : IMessageService
    {
        public async Task ShowMessageAsync(string title, string message)
        {
#if ANDROID
            if (MainActivity.Instance == null) return;
            DialogClickEventArgs? args = null;
            Task dlgRetTask = new(() => { });
            EventHandler<DialogClickEventArgs> dlgBtnhandler = (s, a) =>
            {
                args = a;
                dlgRetTask.Start();
            };
#pragma warning disable CS8602 // 解引用可能出现空引用。
            new AlertDialog.Builder(MainActivity.Instance)
                .SetTitle(title)
                .SetMessage(message)
                .SetPositiveButton("确定", dlgBtnhandler)
                .SetNegativeButton("取消", dlgBtnhandler)
                .SetNeutralButton("中性", dlgBtnhandler)
                .Show();
#pragma warning restore CS8602 // 解引用可能出现空引用。
            await dlgRetTask;
            Toast.MakeText(MainActivity.Instance,
                $"Dialog消失了, which={(DialogButtonType?)args?.Which}", ToastLength.Long)?.Show();
#endif
        }
    }
}