using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;

namespace DataProtectionSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string readOption = "-r";
            const string writeOption = "-w";

            if (args.Length != 2) return;

            MySafe safe = SetupDataProtection();
            switch (args[0])
            {
                case readOption:
                    Read(safe, args[1]);
                    break;
                case writeOption:
                    Write(safe, args[1]);
                    break;
                default: throw new ArgumentException(args[0]);
            }
        }

        static MySafe SetupDataProtection()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo("."))
                .SetDefaultKeyLifetime(TimeSpan.FromDays(20))
                .ProtectKeysWithDpapi();
            serviceCollection.AddSingleton<MySafe>();

            var serviceProvider = serviceCollection.BuildServiceProvider();
            //return ActivatorUtilities.CreateInstance<MySafe>(serviceProvider);
            return serviceProvider.GetRequiredService<MySafe>();
        }

        static void Write(MySafe safe, string fileName)
        {
            string input = Console.ReadLine();
            string encryted = safe.Encrypt(input);
            File.WriteAllText(fileName, encryted);
            Console.WriteLine("content written to {0}", fileName);
        }

        static void Read(MySafe safe, string fileName)
        {
            string encryted = File.ReadAllText(fileName);
            string decryted = safe.Decrypt(encryted);
            Console.WriteLine(decryted);
        }
    }
}