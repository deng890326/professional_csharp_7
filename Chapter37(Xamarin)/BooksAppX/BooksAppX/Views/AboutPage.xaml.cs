using System.Windows.Input;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace BooksAppX.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
            BindingContext = this;
            OpenWebCommand = new Command(() =>
                Launcher.OpenAsync(new Uri("https://csharp.christiannagel.com")));
        }

        public ICommand OpenWebCommand { get; }
    }
}