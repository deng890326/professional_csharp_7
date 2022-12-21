using DataLib;
using ReflectionLib;

namespace LINQCompoundFrom
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<string> ferrariRacers =
                from r in Formula1.GetChampions()
                from c in r.Cars
                where c == "Ferrari"
                orderby r.LastName
                select $"{r.FirstName} {r.LastName}";
            foreach (string ferrariRacer in ferrariRacers)
            {
                Console.WriteLine(ferrariRacer);
            }
            Console.WriteLine();

            var ferrariRacers1 = Formula1.GetChampions()
                .SelectMany(r => r.Cars, (Racer r, string c) => new { Racer = r, Car = c });
            Console.WriteLine($"SelectMany result type info: {ferrariRacers1.GetTypeInfo()}");
            Console.WriteLine();
            ferrariRacers1 = ferrariRacers1
                .Where(r => r.Car == "Ferrari");
            Console.WriteLine($"Where result type info: {ferrariRacers1.GetTypeInfo()}");
            Console.WriteLine();
            ferrariRacers1 = ferrariRacers1
                .OrderBy(r => r.Racer.LastName);
            Console.WriteLine($"OrderBy result type info: {ferrariRacers1.GetTypeInfo()}");
            Console.WriteLine();
            var ferrariRacersResult = ferrariRacers1
                .Select(r => $"{r.Racer.FirstName} {r.Racer.LastName}");
            Console.WriteLine($"Select result type info: {ferrariRacers1.GetTypeInfo()}");
            Console.WriteLine();
            foreach (var ferrariRacer in ferrariRacersResult)
            {
                Console.WriteLine(ferrariRacer);
            }
        }
    }
}