using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace LoggingSample
{
    internal class Program
    {
        public static ServiceProvider SP;

        static Program()
        {
            var services = new ServiceCollection();
            services.AddLogging(ConfigureLogging);
            services.AddScoped<SampleController>();
            SP = services.BuildServiceProvider();
        }

        private static void ConfigureLogging(ILoggingBuilder builder)
        {
            builder.AddConsole(option => option.IncludeScopes = true);
#if DEBUG
            builder.AddDebug();
#else
            builder.AddFilter((cate, level) =>
            {
                if (cate != null && cate.Contains(nameof(SampleController))
                    && level >= LogLevel.Information)
                {
                    return true;
                }
                else if (level >= LogLevel.Error)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            });
#endif
            builder.AddEventSourceLogger();

        }

        static async Task Main(string[] args)
        {
            const string url = "http://www.cninnovation.com";
            var controller = SP.GetRequiredService<SampleController>();
            await controller.NetworkRequestSampleAsync(url);
            await controller.NetworkRequestSampleAsync(url + "1");
            Console.WriteLine();
            Console.WriteLine("using scope:");
            await controller.NetworkRequestSampleUsingLogScopeAsync(url);
            await controller.NetworkRequestSampleUsingLogScopeAsync(url + "2");

            var loggerFactory = LoggerFactory.Create(ConfigureLogging);
            var defaultLogger = loggerFactory.CreateLogger("Default Category");
            defaultLogger.LogInformation("Info Message");
            var programLogger = loggerFactory.CreateLogger<Program>();
            programLogger.LogInformation("Info Message");

            Console.ReadKey();
        }
    }
}