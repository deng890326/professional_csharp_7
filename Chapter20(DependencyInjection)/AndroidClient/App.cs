using Android.Runtime;
using DISampleLib;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidClient
{
    [Application]
    internal class App : Application
    {
        public static App Current => (App)Context;
        public static Activity? CurrentActivity { get; set; }

        public ServiceProvider ServiceProvider { get; private set; }

        public App() : base() => ServiceProvider = initServiceProvider();
        public App(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer) =>
            ServiceProvider = initServiceProvider();

        private ServiceProvider initServiceProvider()
        {
            ServiceCollection services = new ServiceCollection();
            services.AddScoped<ShowMessageViewModel>();
            services.AddSingleton<IMessageService, MessageService>();
            return services.BuildServiceProvider();
        }
    }
}
