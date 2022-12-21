using ResourceDemo.Resources;
using System.Globalization;
using System.Resources;

namespace ResourceDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var resourceManager = new ResourceManager(typeof(Message));
            //var resourceManager = new ResourceManager($"{nameof(ResourceDemo)}." +
            //    $"{nameof(Resources)}.{nameof(Message)}", typeof(Message).Assembly);
            var resourceManager = Message.ResourceManager;
            string? goodMorning = resourceManager.GetString("GoodMorning", new CultureInfo("zh-CN"));
            Console.WriteLine(goodMorning);
            Console.WriteLine(Message.GoodMorning);
            Console.WriteLine(Message.Culture);
        }
    }
}