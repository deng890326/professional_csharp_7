using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithDIContainer
{
    internal class GreetingService : IGreetingService, IDisposable
    {
        public string Greet(string name) => $"Hello, {name}";

        void IDisposable.Dispose() => Console.WriteLine("GreetingService Dispose");

        private GreetingService()
        {
            Console.WriteLine("GreetingService Ctor");
        }

        public class SingletonFactory
        {
            public static readonly GreetingService Instance;

            static SingletonFactory()
            {
                Instance = new GreetingService();
            }
        }
    }
}
