#if ANDROID
using Android.Widget;
#endif

using Microsoft.Toolkit.Uwp.Notifications;

namespace BooksAppMaui.Pages
{
    public partial class HelloPage : ContentPage
    {
        int count = 0;

        public HelloPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public Command<string> Command { get; } = new (str =>
        {
#if ANDROID
            Toast.MakeText(MauiApplication.Current, str ?? "", ToastLength.Short)?.Show();
#elif WINDOWS
            new ToastContentBuilder()
            .AddText(str ?? "empty").Show();
#endif
        });

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }
}