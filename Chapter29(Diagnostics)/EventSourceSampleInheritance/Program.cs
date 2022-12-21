using static EventSourceSampleInheritance.SampleEventSource;

namespace EventSourceSampleInheritance
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            
            Console.WriteLine($"Log Guid: {Log.Guid}");
            Console.WriteLine($"Name: {Log.Name}");
            Log.Startup();
            await NetworkRequestSampleAsync();
            Console.ReadKey();
            Log.Dispose();
        }

        private static async Task NetworkRequestSampleAsync()
        {
            try
            {
                using var client = new HttpClient();
                string url = "http://www.cninnovation.com";
                Log.CallService(url);
                string result = await client.GetStringAsync(url);
                Log.CalledService(url, result.Length);
                Console.WriteLine("Complete....................");
            }
            catch (Exception ex)
            {
                Log.ServiceError(ex.Message, ex.HResult);
                Console.WriteLine(ex.Message);
            }
        }
    }
}