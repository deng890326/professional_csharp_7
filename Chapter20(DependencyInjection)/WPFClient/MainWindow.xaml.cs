using DISampleLib;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            serviceScope = App.Current.ServiceProvider.CreateScope();
            DataContext = serviceScope.ServiceProvider.GetRequiredService<ShowMessageViewModel>();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            serviceScope.Dispose();

        }

        public ShowMessageViewModel ViewModel => (ShowMessageViewModel)DataContext;

        private IServiceScope serviceScope;
    }
}
