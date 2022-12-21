using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WithDIContainer
{
    public class GreetingOptions : IDisposable
    {
        public string? From { get; set; }
        public bool PrintDateTime { get; set; }

        public GreetingOptions() => Console.WriteLine("GreetingOptions Ctor");
        void IDisposable.Dispose() => Console.WriteLine("GreetingOptions Dispose");
    }
}
