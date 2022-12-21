using System.Runtime.CompilerServices;

namespace CallerInfo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            p.Log();
            p.SomeProperty = 3;
            Action a = () => p.Log();
            a();
        }

        public Program()
        {
            Log();
        }

        ~Program()
        {
            Log();
        }

        private int _someProperty;
        public int SomeProperty
        {
            get { return _someProperty; }
            set {
                Log();
                _someProperty = value;
            }
        }

        public void Log([CallerLineNumber] int line = -1,
                        [CallerFilePath] string? path = null,
                        [CallerMemberName] string? name = null)
        {
            Console.WriteLine((line == -1) ? "No line." : $"Line: {line}");
            Console.WriteLine((path == null) ? "No path." : $"Path: {path}");
            Console.WriteLine((name == null) ? "No name." : $"Name: {name}");
        }
    }
}