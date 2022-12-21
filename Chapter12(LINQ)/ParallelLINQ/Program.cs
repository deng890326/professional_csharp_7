using System.Collections.Concurrent;

namespace ParallelLINQ
{
    internal class Program
    {
        static IList<int> sampleData()
        {
            const int arraySize = int.MaxValue >> 1;
            var r = new Random();
            return Enumerable.Range(0, arraySize).Select(i => r.Next(50)).ToList();
        }
        static void Main(string[] args)
        {
            //ParallelLINQ();
            //UseAPatitioner();
            UseCancellation();
        }

        private static void ParallelLINQ()
        {
            DateTime startTime = DateTime.Now;
            Console.WriteLine("ParallelLINQ comupting...");
            var res = (from x in sampleData().AsParallel()
                       where Math.Log(x) < 4
                       select x).Average();
            Console.WriteLine(res);
            Console.WriteLine($"took time: {DateTime.Now - startTime:c}");
            Console.WriteLine();
        }

        private static void UseAPatitioner()
        {
            DateTime startTime = DateTime.Now;
            Console.WriteLine("UseAPatitioner comupting...");
            var res = (from x in Partitioner.Create(sampleData(), true).AsParallel()
                       where Math.Log(x) < 4
                       select x).Average();
            Console.WriteLine(res);
            Console.WriteLine($"took time: {DateTime.Now - startTime:c}");
            Console.WriteLine();
        }

        private static void UseCancellation()
        {
            Console.WriteLine("UseCancellation() starting...");
            var cts = new CancellationTokenSource();
            Task.Run(() =>
            {
                try
                {
                    Console.WriteLine("task running...");
                    var res = (from x in sampleData().AsParallel().WithCancellation(cts.Token)
                               where Math.Log(x) < 4
                               select x).Average();
                    Console.WriteLine($"task finished with {res}");
                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("task canceled.");
                    Console.WriteLine(ex.ToString());
                }
            });

            Thread.Sleep(1000);
            string input;
            do
            {
                Console.Write("Cancel?");
                input = Console.ReadLine()?.ToUpper() ?? "";
            }
            while (input != "Y" && input != "YES");
            cts.Cancel();
            Thread.Sleep(1000);
        }
    }
}