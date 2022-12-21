using SharedProject;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Message.Show($"From .NET CORE! {Message.Add(100, 1)}");
        }
    }
}