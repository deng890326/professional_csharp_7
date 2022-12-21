#if ANDROID
using Android.Widget;
#endif
using BooksAppMaui.Pages;
using BooksAppMaui.Services;
using BooksLib.Models;
using BooksLib.Repos;
using BooksLib.Services;
using BooksLib.ViewModels;
using Framewok.Services;
using Microsoft.Maui.LifecycleEvents;

namespace BooksAppMaui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .ConfigureLifecycleEvents(lifecycleBuilder =>
                {
#if ANDROID
                    lifecycleBuilder.AddAndroid(androidBuilder =>
                    {
                        androidBuilder.OnResume(activity =>
                        {
                            Toast.MakeText(activity, $"{activity.GetType()} Resumed!!!", ToastLength.Long)?.Show();
                        });
                    });
#endif
                })
                .Services
                .AddSingleton<IBooksRepository, BooksRepository>()
                .AddSingleton<IItemsService<Book>, BooksService>()
                .AddSingleton<INavigationService, BookNavigationService>()
                .AddSingleton<IMessageService, AppMessageService>()
                .AddSingleton<BooksPage>()
                .AddSingleton<BookMaterDetailViewModel>()
                .AddTransient<BookDetailPage>()
                .AddTransient<BookDetailViewModel>()
                .AddSingleton<HelloPage>()
                .AddLogging()
                .AddOptions<BooksService.Options>()
                .Configure(op => op.AutoRefresh = true);

            return builder.Build();
        }

        public static TService GetRequiredService<TService>()
            where TService : notnull
        {
            return GetServiceProvider().GetRequiredService<TService>();
        }

        public static TService? GetService<TService>()
            where TService : notnull
        {
            return GetServiceProvider().GetService<TService>();
        }

        private static IServiceProvider GetServiceProvider()
        {
            IServiceProvider? services =
#if ANDROID
                MauiApplication.Current.Services;
#elif WINDOWS
                MauiWinUIApplication.Current.Services;
#elif IOS || MACCATALYST
                MauiUIApplicationDelegate.Current.Services;
#else
                null;
#endif
            return services ?? throw new NotSupportedException();
        }
    }
}