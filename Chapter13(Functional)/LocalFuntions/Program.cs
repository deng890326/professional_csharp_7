using Extensions;
using ReflectionLib;

namespace LocalFuntions
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] names = { "James", "Niki" };

            try
            {
                var q = names.Where1(null);
                Console.WriteLine($"Where1 return type info = ({q.GetTypeInfo()})");
                foreach (var name in q)
                {
                    Console.WriteLine(name);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Where1:");
                Console.WriteLine(ex.ToString());
                Console.WriteLine();
            }

            try
            {
                var q = names.Where2(_ => true);
                Console.WriteLine($"Where2 return type info = ({q.GetTypeInfo()})");
                q = names.Where2(null);
                foreach (var name in q)
                {
                    Console.WriteLine(name);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Where2:");
                Console.WriteLine(ex.ToString());
                Console.WriteLine();
            }

            try
            {
                var q = names.Where3(_ => true);
                Console.WriteLine($"Where3 return type info = ({q.GetTypeInfo()})");
                q = names.Where3(null);
                foreach (var name in q)
                {
                    Console.WriteLine("Where3:");
                    Console.WriteLine(name);
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}