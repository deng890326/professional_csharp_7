namespace ValuleTaskSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 20; i++)
            {
                var task = GetSomeDataAsync();
                Console.WriteLine(task.GetType().FullName);
                task.AsTask().Wait();
                Thread.Sleep(1000);
            }
        }

        public static Task<(IEnumerable<string> data, DateTime time)>
            GetTheRealData() =>
            Task.FromResult((Enumerable.Range(0, 10).Select(i => $"item {i}"), DateTime.Now));

        private static DateTime lastTime = DateTime.MinValue;
        private static IEnumerable<string> data = Enumerable.Empty<string>();

        public static async ValueTask<IEnumerable<string>> GetSomeDataAsync()
        {
            if (lastTime >= DateTime.Now.AddSeconds(-5))
            {
                Console.WriteLine("data from the cache");
                return await new ValueTask<IEnumerable<string>>(data);
            }

            Console.WriteLine("data from the service");
            (data, lastTime) = await GetTheRealData();
            return data;
        }
    }
}