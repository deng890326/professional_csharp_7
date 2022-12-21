using DataLib;

namespace LINQJoin
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var racers = from r in Formula1.GetChampions()
                         from y in r.Years
                         select (year: y, name: $"{r.FirstName} {r.LastName}");

            var teams = from t in Formula1.GetConstructorChampions()
                        from y in t.Years
                        select (year: y, name: t.Name);
            //Console.WriteLine("InnerJoin:");
            //InnerJoin(racers, teams);
            //Console.WriteLine("LeftOuterJoin:");
            //LeftOuterJoin(racers, teams);
            //Console.WriteLine("GroupJoin:");
            //GroupJoin();
            Console.WriteLine("SelectManyTest:");
            SelectManyTest();
        }

        private static void InnerJoin(IEnumerable<(int year, string name)> racers,
                                      IEnumerable<(int year, string name)> teams)
        {
            var racersAndTeams =
                from r in racers
                join t in teams on r.year equals t.year // 内连接
                orderby r.year
                select (r.year, champion: r.name, constructor: t.name);
            racersAndTeams = racersAndTeams.Take(10);

            Print(racersAndTeams);


            var racersAndTeams2 =
                racers.Join(inner: teams,
                            outerKeySelector: r => r.year,
                            innerKeySelector: t => t.year,
                            resultSelector: (r, t) => (r.year, champion: r.name, constructor: t.name))
                      .OrderByDescending(rat => rat.year)
                      .Take(10);
            Print(racersAndTeams2);
        }

        private static void LeftOuterJoin(IEnumerable<(int year, string name)> racers,
                                          IEnumerable<(int year, string tname)> teams)
        {
            var racersAndTeams = (
                from r in racers
                join t in teams on r.year equals t.year into ts
                from t in ts.DefaultIfEmpty()
                orderby r.year
                select (r.year, champion: r.name, constructor: t.tname ?? "null")
            ).Take(10);
            Print(racersAndTeams);

            var racersAndTeams2 =
                racers.GroupJoin(inner: teams,
                                 outerKeySelector: racer => racer.year,
                                 innerKeySelector: team => team.year,
                                 resultSelector: (racer, teams) => (racer.year, champion: racer.name, teams))
                      .SelectMany(collectionSelector: g => g.teams.DefaultIfEmpty(),
                                  resultSelector: (g, team) => (g.year, g.champion, constructor: team.tname ?? "null"))
                      .OrderBy(g => g.year)
                      .Take(10);
            Print(racersAndTeams2);

            var racersAndTeams3 =
                racers.GroupJoin(inner: teams,
                                 outerKeySelector: r => r.year,
                                 innerKeySelector: t => t.year,
                                 resultSelector: (r, ts) => (r.year, champion: r.name, constructor: ts.FirstOrDefault().tname ?? "null"))
                      .OrderBy(g => g.year)
                      .Take(10);
            Print(racersAndTeams3);
        }

        private static void GroupJoin()
        {
            var racers =
                from cs in Formula1.GetChampionships()
                from r in new List<(int year, int pos, string name)>
                {
                    (cs.Year, 1, cs.First),
                    (cs.Year, 2, cs.Second),
                    (cs.Year, 3, cs.Third)
                }
                select r;
            var q =
                from r in Formula1.GetChampions()
                join r1 in racers on $"{r.FirstName} {r.LastName}" equals r1.name into r1s
                select (Racer: r, Results: r1s.DefaultIfEmpty());
            foreach (var r in q)
            {
                Console.WriteLine($"{r.Racer:A}");
                Console.WriteLine($"prizes: {string.Join(",\n\t", r.Results)}");
                Console.WriteLine();
            }
        }

        private static void GroupJoinWithMethod()
        {
            var racers =
                Formula1.GetChampionships()
                .SelectMany(selector: cs => new List<(int year, int pos, string name)>
                {
                    (cs.Year, 1, cs.First),
                    (cs.Year, 2, cs.Second),
                    (cs.Year, 3, cs.Third),
                });
            var q =
                Formula1.GetChampions()
                .GroupJoin(inner: racers,
                           outerKeySelector: r => $"{r.FirstName} {r.LastName}",
                           innerKeySelector: r1 => r1.name,
                           resultSelector: (r, r1s) => (Racer: r, Results: r1s.DefaultIfEmpty()));
            foreach (var r in q)
            {
                Console.WriteLine($"{r.Racer:A}");
                Console.WriteLine($"prizes: {string.Join(",\n\t", r.Results)}");
                Console.WriteLine();
            }
        }

        private static void SelectManyTest()
        {
            var racerGroups =
                Formula1.GetChampionships()
                .SelectMany(collectionSelector: cs =>
                {
                    return new List<(int year, int pos, string name)>
                    {
                        (cs.Year, 1, cs.First),
                        (cs.Year, 2, cs.Second),
                        (cs.Year, 3, cs.Third)
                    };
                }, resultSelector: (cs, r) => (Racer: r, cs.First, cs.Second, cs.Third))
                .GroupBy(keySelector: r => r.Racer.name,
                         resultSelector: (key, rs) => (name: key, prizes: rs.Select(rs =>
                         {
                             return (rs.Racer.year, rs.Racer.pos, alongWith: ($"1st: {rs.First}", $"2nd: {rs.Second}", $"3rd: {rs.Third}"));
                         })));
            foreach (var rg in racerGroups)
            {
                Console.WriteLine(rg.name);
                foreach (var r in rg.prizes)
                {
                    Console.WriteLine($"\t{r}");
                }
            }
            Console.WriteLine();
        }

        private static void Print(IEnumerable<(int year, string champion, string constructor)> racersAndTeams)
        {
            foreach (var rat in racersAndTeams)
            {
                Console.WriteLine($"{rat.year}: {rat.champion,-20} {rat.constructor}");
            }
            Console.WriteLine();
        }
    }
}