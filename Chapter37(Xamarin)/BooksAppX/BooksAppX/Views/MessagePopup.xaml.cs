using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BooksAppX.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MessagePopup : PopupPage
    {
        public MessagePopup(string title, string message)
        {
            InitializeComponent();
            TitleLabel.Text = title;
            MessageLabel.Text = message;
        }
    }
}