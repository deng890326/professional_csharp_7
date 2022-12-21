using static DataLib.Enumerables;

namespace SimpleLINQJoin
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var join1 =
                from it1 in Items1
                join it2 in Items2 on it1.key1 equals it2.key2
                select (it1.key1, it1.value1, it2.key2, it2.value2 ?? "null");
            Console.WriteLine("join1:");
            foreach (var it in join1)
            {
                Console.WriteLine(it);
            }
            Console.WriteLine();

            var join2 = 
                from it1 in Items1
                join it2 in Items2 on it1.key1 equals it2.key2 into it2s
                from it2 in it2s.DefaultIfEmpty()
                select (it1.key1, it1.value1, it2.key2, it2.value2 ?? "null");
            Console.WriteLine("join2:");
            foreach (var it in join2)
            {
                Console.WriteLine(it);
            }
            Console.WriteLine();

            var joinMethod =
                Items1.Join(inner: Items2,
                            outerKeySelector: it1 => it1.key1,
                            innerKeySelector: it2 => it2.key2,
                            resultSelector: (it1, it2) => (it1.key1, it1.value1, it2.key2, it2.value2 ?? "null"));
            Console.WriteLine("joinMethod:");
            foreach (var it in joinMethod)
            {
                Console.WriteLine(it);
            }
            Console.WriteLine();

            var groupJoinMethod =
                Items1.GroupJoin(inner: Items2,
                                 outerKeySelector: it1 => it1.key1,
                                 innerKeySelector: it2 => it2.key2,
                                 resultSelector: (it1, it2s) => (it1.key1, it1.value1, it2s));
            Console.WriteLine("groupJoinMethod:");
            foreach (var it in groupJoinMethod)
            {
                Console.WriteLine($"{it.key1}, {it.value1}, [{string.Join(", ", it.it2s)}]");
            }
            Console.WriteLine();

            var groupJoinSelectManyWithDefault =
                groupJoinMethod.SelectMany(collectionSelector: g => g.it2s.DefaultIfEmpty(),
                                           resultSelector: (g, it2) => (g.key1, g.value1, it2.key2, it2.value2 ?? "null"));
            Console.WriteLine("groupJoinSelectManyWithDefault:");
            foreach (var it in groupJoinSelectManyWithDefault)
            {
                Console.WriteLine(it);
            }
            Console.WriteLine();

            var groupJoinSelectManyNoDefault =
                groupJoinMethod.SelectMany(collectionSelector: g => g.it2s,
                                           resultSelector: (g, it2) => (g.key1, g.value1, it2.key2, it2.value2 ?? "null"));
            Console.WriteLine("groupJoinSelectManyNoDefault:");
            foreach (var it in groupJoinSelectManyNoDefault)
            {
                Console.WriteLine(it);
            }
            Console.WriteLine();
        }
    }
}