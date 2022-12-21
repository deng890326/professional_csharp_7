namespace ParallelSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //test1();
            //test2();
            //stopParallelForEarly();
            //parallelForWithInit();
            //parallelForEach();
            parallelInvoke();
            Console.ReadLine();
        }

        private static void test1()
        {
            DateTime start = DateTime.Now;
            ParallelLoopResult result = Parallel.For(0, 10, i =>
            {
                Log($"Start {i}");
                Thread.Sleep(100);
                Log($"End   {i}");
            });
            DateTime end = DateTime.Now;
            Console.WriteLine($"Is completed: {result.IsCompleted}\n" +
                $"time span: {end - start:c}");
        }

        private static void test2()
        {
            DateTime start = DateTime.Now;
            ParallelLoopResult result = Parallel.For(0, 10, async i =>
            {
                Log($"Start {i}");
                await Task.Delay(100);
                Log($"End   {i}");
            });
            DateTime end = DateTime.Now;
            Console.WriteLine($"Is completed: {result.IsCompleted}\n" +
                $"time span: {end - start:c}");
        }

        private static void stopParallelForEarly()
        {
            ParallelLoopResult result = Parallel.For(10, 40, (i, pls) =>
            {
                Log($"Start {i}");
                if (i > 12)
                {
                    pls.Break();
                    Log($"break {i}");
                }
                Thread.Sleep(100);
                Log($"End   {i}");
            });
            Console.WriteLine($"Is completed: {result.IsCompleted}\n" +
                $"LowestBreakIteration: {result.LowestBreakIteration}");
        }

        private static void parallelForWithInit()
        {
            ParallelLoopResult result = Parallel.For<long>(0, 10, () =>
            {
                Log("init thread");
                return 0;
            },
            (i, pls, local) =>
            {
                Log($"body thread, i: {i}, local:{local}");
                Thread.Sleep(100);
                return local + i;
            },
            local =>
            {
                Log($"finally thread, local: {local}");
            });
        }

        static void parallelForEach()
        {
            string[] data = new string[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            Parallel.ForEach(data, s =>
            {
                Log(s);
            });
        }

        static void parallelInvoke()
        {
            Parallel.Invoke(
                () => Log("Foo"),
                () => Log("Bar"));
        }

        static void Log(string prefix) =>
            Console.WriteLine($"{prefix} task: {Task.CurrentId}, " +
                $"thread: {Thread.CurrentThread.ManagedThreadId}");
    }
}