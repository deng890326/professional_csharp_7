using static Extensions.FunctionExtensions;

namespace UsingStatic
{
    internal class Program
    {
        static void Main(string[] args) => new Resource().Use(res => res.Use(res => res.Foo()));
    }

    class Resource : IDisposable
    {
        public bool Disposed { get; private set; }

        public void Foo()
        {
            Func<int, double> f1 = x => x * 2.1;
            Func<double, string> f2 = x => $"{x + 5}";
            Console.WriteLine($"{nameof(Foo)}: {Compose(f1, f2)(4)}");
        }

        public void Dispose()
        {
            Dispose(true);
        }
        
        public virtual void Dispose(bool disposing)
        {
            Console.WriteLine($"{nameof(Disposed)} = {Disposed}");
            if (!Disposed)
            {
                if (disposing)
                {
                    Console.WriteLine("release resource");
                }
                Disposed = true;
            }
        }
    }
}