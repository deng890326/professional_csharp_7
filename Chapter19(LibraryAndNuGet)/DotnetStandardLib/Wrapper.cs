using DotnetFrameworkLib;

namespace DotnetStandardLib
{
    public class Wrapper
    {
        public static void ConsoleMessage(string message) =>
            Legacy.ConsoleMessage(message);

        public static void ShowConsoleType() =>
            Legacy.ShowConsoleType();

        public static void WindowsFormsMessage(string message) =>
            Legacy.WindowsFormsMessage(message);
    }
}
