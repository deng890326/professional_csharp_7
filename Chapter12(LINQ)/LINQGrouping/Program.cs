using DataLib;
using ReflectionLib;
using System.Diagnostics.Metrics;

namespace LINQGrouping
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var countries = from r in Formula1.GetChampions()
                            group r by r.Country into g
                            orderby g.Count() descending, g.Key
                            where g.Count() >= 2
                            select (g.Key, g.Count());

            object? countryObj = null;
            foreach (var country in countries)
            {
                countryObj = country;
                Console.WriteLine($"{country.Key,-10} {country.Item2}");
            }
            Console.WriteLine($"countryObj typeinfo={{{countryObj?.GetTypeInfo()}}}");
            Console.WriteLine();

            var countriesResult = Formula1.GetChampions()
                .GroupBy(r => r.Country);
            Console.WriteLine($"GroupBy result typeinfo={{{countriesResult.GetTypeInfo()}}}\n");
            countriesResult = countriesResult
                .OrderBy(g => g.Count())
                .ThenByDescending(g => g.Key);
            Console.WriteLine($"ThenBy result typeinfo={{{countriesResult.GetTypeInfo()}}}\n");
            countriesResult = countriesResult
                .Where(g => g.Count() >= 2);
            Console.WriteLine($"Where result typeinfo={{{countriesResult.GetTypeInfo()}}}\n");
            IGrouping<string, Racer> group1 = countriesResult.Last();
            Console.WriteLine($"group1={{{ToString(group1)}}},\n\ttypeinfo: {group1.GetTypeInfo()}}}\n");
            var countries1 = countriesResult
                .Select(g => (g.Key, g.Count()));
            Console.WriteLine($"Select result typeinfo={{{countries1.GetTypeInfo()}}}\n");
            foreach (var country in countries1)
            {
                Console.WriteLine($"{country.Key,-10} {country.Item2}");
            }
            Console.WriteLine();

            Console.WriteLine("GroupingWithVariables:");
            GroupingWithVariables();
            Console.WriteLine();

            Console.WriteLine("GroupingAndNestedObjects:");
            GroupingAndNestedObjects();
            Console.WriteLine();
        }

        static void GroupingWithVariables()
        {
            var countries = from r in Formula1.GetChampions()
                            group r by r.Country into g
                            let count = g.Count() //扩展了Group，增加了一个count字段
                            let country = g.Key
                            orderby count descending, country
                            where count >= 2
                            select (country, count, g);
            foreach (var country in countries)
            {
                Console.WriteLine($"{{{country.country,-10}, {country.count}, {ToString(country.g)}}}");
            }

            var countries2 = Formula1.GetChampions()
                .GroupBy(r => r.Country)
                .Select(group => (country: group.Key, count: group.Count()))
                .OrderBy(t => t.count)
                .ThenByDescending(t => t.country)
                .Where(t => t.count >= 2);
            foreach (var country in countries2)
            {
                Console.WriteLine($"{{{country.country,-10}, {country.count}}}");
            }
        }

        static void GroupingAndNestedObjects()
        {
            var countries = from r in Formula1.GetChampions()
                            group r by r.Country into g
                            let country = g.Key
                            let count = g.Count()
                            orderby count descending, country
                            where count >= 2
                            select (country, count,
                                racers: from r1 in g
                                        orderby r1.FirstName
                                        select r1.FirstName + " " + r1.LastName);
            object? obj = null;
            foreach (var country in countries)
            {
                obj = country;
                Console.WriteLine($"{{{country.country,-10}, {country.count}, {string.Join("; ", country.racers)}}}");
            }
            //Console.WriteLine($"obj type={obj?.GetType()}");
            Console.WriteLine();

            var countries2 = Formula1.GetChampions()
                .GroupBy(r => r.Country)
                .Select(g => (country: g.Key,
                              count: g.Count(),
                              racers: g.OrderBy(r => r.FirstName)
                                       .Select(r => r.FirstName + " " + r.LastName)))
                .OrderBy(t => t.count)
                .ThenByDescending(t => t.country)
                .Where(t => t.count >= 2);
            foreach (var country in countries2)
            {
                obj = country;
                Console.WriteLine($"{{{country.Item1,-10}, {country.Item2}, {string.Join("; ", country.Item3)}}}");
            }
            //Console.WriteLine($"obj type={obj?.GetType()}");

        }

        private static string ToString(IGrouping<string, Racer> group)
        {
            return $"Key={group.Key},Racers=[{string.Join(",\n\t", group.Select(r => $"{{{r:A}}}"))}]";
        }
    }
}