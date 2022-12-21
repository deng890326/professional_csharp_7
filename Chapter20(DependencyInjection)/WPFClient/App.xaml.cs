using DISampleLib;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WPFClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static new App Current => (App)Application.Current;

        public ServiceProvider ServiceProvider { get; private set; }

        private ServiceProvider initServiceProvider()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddScoped<ShowMessageViewModel>();
            services.AddSingleton<IMessageService, MessageService>();
            return services.BuildServiceProvider();
        }

        App() : base() => ServiceProvider = initServiceProvider();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        }
    }
}
