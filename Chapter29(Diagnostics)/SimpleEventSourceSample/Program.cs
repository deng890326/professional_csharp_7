using System.Diagnostics.Tracing;

namespace SimpleEventSourceSample
{
    internal class Program
    {
        private static EventSource sample =
            new EventSource("SampleEvent");

        static async Task Main(string[] args)
        {
            Console.WriteLine($"Log Guid: {sample.Guid}");
            Console.WriteLine($"Name: {sample.Name}");
            sample.Write(nameof(Main), new { Info = "some message." });
            await NetworkRequestSampleAsync();
            Console.ReadKey();
            sample.Dispose();
        }

        private static async Task NetworkRequestSampleAsync()
        {
            try
            {
                using var client = new HttpClient();
                string url = "http://www.cninnovation.com";
                sample.Write("Network", $"requesting {url}");
                string result = await client.GetStringAsync(url);
                sample.Write("Network", $"completed call to {url}," +
                    $" result string length: {result.Length}");
                Console.WriteLine("Complete....................");
            }
            catch (Exception ex)
            {
                sample.Write("Network Error",
                    new EventSourceOptions() { Level = EventLevel.Error },
                    (ex.Message, Result: ex.HResult));
                Console.WriteLine(ex.Message);
            }
        }
    }
}