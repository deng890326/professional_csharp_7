using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithDIContainer
{
    public class HomeController : IDisposable
    {
        public HomeController(IGreetingService greetingService, IOptions<GreetingOptions>? options)
        {
            Console.WriteLine("HomeController Ctor");
            this.greetingService = greetingService;
            this.greetingOptions = options?.Value;
        }

        public void Hello(string name)
        {
            Console.WriteLine(greetingService.Greet(name));
            if (greetingOptions?.From is string from)
            {
                Console.WriteLine($"From: {from}");
            }
            if (greetingOptions?.PrintDateTime is true)
            {
                Console.WriteLine($"Now: {DateTime.Now:G}");
            }
        }

        public bool? GreetingShowDateTime => greetingOptions?.PrintDateTime;

        void IDisposable.Dispose() => Console.WriteLine("HomeController Dispose");

        private IGreetingService greetingService;
        private GreetingOptions? greetingOptions;
    }
}
