using DataLib;
using ReflectionLib;

namespace LINQSorting
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var racers = from r in Formula1.GetChampions()
                         where r.Country == "Brazil"
                         orderby r.Wins descending
                         select r;
            foreach (Racer r in racers)
            {
                Console.WriteLine(r.ToString("A"));
            }
            Console.WriteLine();

            var racers1 = (from r in Formula1.GetChampions()
                           orderby r.Country descending, r.FirstName, r.LastName
                           select r).Take(10);
            foreach (Racer r in racers1)
            {
                Console.WriteLine(r.ToString("A"));
            }
            Console.WriteLine();

            var racers2 = (from r in Formula1.GetChampions()
                          .OrderBy(r => r.Country)
                          .ThenBy(r => r.FirstName)
                          .ThenBy(r => r.LastName)
                          select r).Take(10);
            Console.WriteLine($"racers2 typeinfo: {racers2.GetTypeInfo()}");
            foreach (Racer r in racers2)
            {
                Console.WriteLine(r.ToString("A"));
            }
            Console.WriteLine();
        }
    }
}