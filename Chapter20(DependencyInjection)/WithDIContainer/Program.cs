using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace WithDIContainer
{
    internal class Program
    {

        public static readonly ServiceProvider GlobalServiceProvider;
        public static readonly IConfigurationRoot Configuration;

        static Program()
        {
            var services = new ServiceCollection();

            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            GlobalServiceProvider = services
                .AddOptions()
                .AddSingleton<IGreetingService>(_ => GreetingService.SingletonFactory.Instance)
                //.Configure<GreetingOptions>(o => { o.From = "Program"; o.PrintDateTime = true; })
                //.ConfigureAll<GreetingOptions>(o => { o.From = "Program"; o.PrintDateTime = true; })
                //.Configure<GreetingOptions>(o =>
                //{
                //    var c = Configuration.GetSection("Greeting");
                //    o.From = c.GetValue<string>("From");
                //    o.PrintDateTime = c.GetValue<bool>("PrintDateTime");
                //})
                .Configure<GreetingOptions>(Configuration.GetSection("Greeting"))
                .AddScoped<HomeController>()
                //.AddTransient<HomeController>()
                .BuildServiceProvider();

            foreach (var service in services)
            {
                Console.WriteLine(service.ToString());
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine($"\n{nameof(Main)} start");
            try
            {
                Console.WriteLine("\nbegin default scope");
                GlobalServiceProvider.GetService<HomeController>()?.Hello("ServiceProvider.GetService()");
                GlobalServiceProvider.GetRequiredService<HomeController>().Hello("ServiceProvider.GetRequiredService()");

                using (IServiceScope scope1 = GlobalServiceProvider.CreateScope())
                {
                    Console.WriteLine("\nbegin scope1");
                    scope1.ServiceProvider.GetRequiredService<HomeController>().Hello("scop1");
                    scope1.ServiceProvider.GetRequiredService<HomeController>().Hello("scop1");
                    Console.WriteLine("end scope1");
                }

                using (AsyncServiceScope scope2 = GlobalServiceProvider.CreateAsyncScope())
                {
                    Console.WriteLine("\nbegin scope2");
                    scope2.ServiceProvider.GetRequiredService<HomeController>().Hello("scop2");
                    scope2.ServiceProvider.GetRequiredService<HomeController>().Hello("scop2");
                    Console.WriteLine("end scope2");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                Console.WriteLine("end default scope");
                GlobalServiceProvider.Dispose();
                Console.WriteLine($"\n{nameof(Main)} end");
            }
        }
    }
}