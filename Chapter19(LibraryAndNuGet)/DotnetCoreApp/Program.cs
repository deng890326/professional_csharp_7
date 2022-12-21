using DotnetFrameworkLib;

namespace DotnetCoreApp
{
    internal class Program
    {
        private const string Message = "Hello from .NET Core";

        static void Main(string[] args)
        {
            Legacy.ConsoleMessage(Message);
            Legacy.ShowConsoleType();
            try
            {
                Legacy.WindowsFormsMessage(Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}