using DataLib;

namespace LINQSamples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //SetOperation();
            //ZipOperation();
            //Partitioning();
            //AggregateCount();
            //AggregateSum();
            //ToLookup();
            CovertWithCast();
        }

        private static void SetOperation()
        {
            static IEnumerable<Racer> racersByCar(string car) =>
                from r in Formula1.GetChampions()
                from c in r.Cars
                where c == car
                select r;

            string car1 = "Ferrari", car2 = "McLaren";
            Console.WriteLine($"World champion with {car1} and {car2}");
            foreach (var racer in racersByCar(car1).Intersect(racersByCar(car2)))
            {
                Console.WriteLine(racer);
            }
        }

        private static void ZipOperation()
        {
            static IEnumerable<Racer> racerRankingByCountry(string country) =>
                from r in Formula1.GetChampions()
                where r.Country == country
                orderby r.Wins descending
                select r;

            string c1 = "UK", c2 = "Germany";
            IEnumerable<Racer> racerRankingC1 = racerRankingByCountry(c1);
            Console.WriteLine($"{c1}:");
            foreach (var racer in racerRankingC1)
            {
                Console.WriteLine($"{racer:A}");
            }
            IEnumerable<Racer> racerRankingC2 = racerRankingByCountry(c2);
            Console.WriteLine($"{c2}:");
            foreach (var racer in racerRankingC2)
            {
                Console.WriteLine($"{racer:A}");
            }
            var racerRankings = racerRankingC1.Zip(racerRankingC2,
                (first, second) => $"({first:A}) vs ({second:A})");
            Console.WriteLine("After Zip:");
            foreach (var rr in racerRankings)
            {
                Console.WriteLine(rr);
            }
        }

        private static void Partitioning()
        {
            int pageSize = 10;
            int totalSize = Formula1.GetChampions().Count();
            int numOfPages = totalSize / pageSize;
            if (totalSize % pageSize != 0)
            {
                numOfPages++;
            }

            for (int i = 0; i < numOfPages; i++)
            {
                Console.WriteLine($"Page {i + 1}:");
                var racers = (
                    from r in Formula1.GetChampions()
                    orderby r.Wins descending
                    select r)
                    .Skip(i * pageSize)
                    .Take(pageSize);
                foreach (var racer in racers)
                {
                    Console.WriteLine($"{racer:A}");
                }
                Console.WriteLine();
            }
        }

        static void AggregateCount()
        {
            var racersOfMostYears =
                from r in Formula1.GetChampions()
                let name = r.ToString()
                let timesChampion = r.Years.Count()
                where timesChampion >= 3
                orderby timesChampion descending, name
                select (name, timesChampion, r.Wins);
            foreach (var racer in racersOfMostYears)
            {
                Console.WriteLine(racer);
            }
            Console.WriteLine();

            var racersOfMostYears2 =
                Formula1.GetChampions()
                .Where(r => r.Years.Count() >= 3)
                .OrderByDescending(r => r.Years.Count())
                .ThenBy(r => r.ToString())
                .Select(r => (r.ToString(), r.Years.Count(), r.Wins));
            foreach (var racer in racersOfMostYears2)
            {
                Console.WriteLine(racer);
            }
            Console.WriteLine();
        }

        private static void AggregateSum()
        {
            var countryWinsRanking = (
                from c in
                    from r in Formula1.GetChampions()
                    group r by r.Country into c
                    let name = c.Key
                    let wins = (from r1 in c select r1.Wins).Sum()
                    let wins1 = c.Sum(r => r.Wins)
                    let wins2 = c.Aggregate(0, (s, r) => s + r.Wins)
                    let wins3 = (from r1 in c select r1.Wins).Aggregate((s, i) => s + i)
                    select (name, wins, wins1, wins2, wins3)
                orderby c.wins descending, c.name
                select c)
                .Take(50);
            foreach (var country in countryWinsRanking)
            {
                Console.WriteLine(country);
            }
        }

        private static void ToLookup()
        {
            var racerLookup = (
                from r in Formula1.GetChampions()
                from c in r.Cars
                select (car: c, racer: r.ToString())
               ).ToLookup(keySelector: cr => cr.car,
                          elementSelector: cr => cr.racer);
            foreach (var racers in racerLookup)
            {
                Console.WriteLine($"car={racers.Key}:");
                foreach (var item in racers)
                {
                    Console.WriteLine($"\t{item}");
                }
            }
        }

        private static void CovertWithCast()
        {
            var list = Array.CreateInstance(typeof(object), 10);
            Array.Copy(Formula1.GetChampions().ToArray(), list, list.Length - 1);
            list.SetValue("string", list.Length - 1);

            var queryWithCast = from r in list.Cast<Racer>()
                        orderby r?.Wins descending
                        select r;
            try
            {
                foreach (var r in queryWithCast)
                {
                    Console.WriteLine($"{r:A}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine();

            var queryWithOfType = from r in list.OfType<Racer>()
                                orderby r.Wins descending
                                select r;
            try
            {
                foreach (var r in queryWithOfType)
                {
                    Console.WriteLine($"{r:A}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine();

            foreach (int i in Enumerable.Range(1, 20).Select(i => i * 3))
            {
                Console.Write($"{i}  ");
            }
            Console.WriteLine();
        }
    }
}