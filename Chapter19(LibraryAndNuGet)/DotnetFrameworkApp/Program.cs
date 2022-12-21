using DotnetStandardLib;
using System;

namespace DotnetFrameworkApp
{
    internal class Program
    {
        private const string Message = "Hello from .NET Framework";

        static void Main(string[] args)
        {
            Wrapper.ConsoleMessage(Message);
            Wrapper.ShowConsoleType();
            try
            {
                Wrapper.WindowsFormsMessage(Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
