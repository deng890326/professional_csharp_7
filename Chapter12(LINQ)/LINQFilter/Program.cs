using DataLib;
using ReflectionLib;

namespace LINQFilter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var racers = from r in Formula1.GetChampions()
                         where r.Wins > 15 && (r.Country == "Brazil" || r.Country == "Austrial")
                         select r;
            foreach (var rac in racers)
            {
                Console.WriteLine($"{rac:A}");
            }
            Console.WriteLine();

            racers = from r in Formula1.GetChampions()
                     .Where((r, index) => r.LastName.StartsWith("A") && index % 2 != 0)
                     select r;
            foreach (var rac in racers)
            {
                Console.WriteLine($"{rac:A}");
            }
            Console.WriteLine();

            object[] data = { "one", 2, 3, "four", "five", 6 };
            var query = data.OfType<string>();
            Console.WriteLine($"query typeinfo: {query.GetTypeInfo()}");
            foreach (var item in query)
            {
                Console.WriteLine(item);
            }
        }
    }
}