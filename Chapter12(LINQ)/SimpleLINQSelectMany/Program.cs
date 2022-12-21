using static DataLib.Enumerables;

namespace SimpleLINQSelectMany
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var selectMany1 =
                Items1.SelectMany(selector: it1 => Items2);
            Console.WriteLine("selectMany1:");
            foreach (var item in selectMany1)
            {
                Console.WriteLine(item);
            }

            var selectMany2 =
                Items1.SelectMany(selector: (it1, i) =>
                {
                    Console.WriteLine(i); return Items2;
                });
            Console.WriteLine("selectMany2:");
            foreach (var item in selectMany2)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            var selectMany3 =
                Items1.SelectMany(collectionSelector: it1 => Items2,
                                  resultSelector: (it1, it2) => (it1.key1, it1.value1, it2.key2, it2.value2 ?? null));
            Console.WriteLine("selectMany3:");
            foreach (var item in selectMany3)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();
        }
    }
}