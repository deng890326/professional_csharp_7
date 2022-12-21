using BooksLib.Models;
using BooksLib.Repos;
using BooksLib.Services;
using BooksLib.ViewModels;
using Framewok.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace BooksAppX
{
    internal static class AppServices
    {
        public static void Init()
        {
            var services = new ServiceCollection();
            services.AddOptions<BooksService.Options>()
                .Configure(op => op.AutoRefresh = true);
            services.AddSingleton<IBooksRepository, BooksRepository>()
                .AddSingleton<IItemsService<Book>, BooksService>()
                .AddSingleton<INavigationService>(AppNavigationService.Create)
                .AddSingleton<IMessageService, AppMessageService>()
                .AddSingleton<BookMaterDetailViewModel>()
                .AddTransient<BookDetailViewModel>()
                .AddLogging(ConfigureLogging);
            Provider = services.BuildServiceProvider();
        }

        public static TService GetRequired<TService>()
        {
            return Provider.GetRequiredService<TService>();
        }

        public static IServiceProvider Provider { get; private set; }

        private static void ConfigureLogging(ILoggingBuilder builder)
        {
        }
    }
}
