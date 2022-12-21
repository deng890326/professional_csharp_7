using System.Diagnostics;
using System.Net;

internal class Program
{
    public static async Task Main(string[] args)
    {
        SynchronizedAPI();
        AsynchronousPattern();
        EventBasedAsyncPattern();
        await TaskBasedAsyncPattern();
        Console.WriteLine(await TaskBasedAsyncPattern2());
    }

    const string url = "http://www.cninnovation.com";

    static void SynchronizedAPI()
    {
        Console.WriteLine($"{nameof(SynchronizedAPI)}");
        using (var client = new WebClient())
        {
            string content = client.DownloadString(url);
            Console.WriteLine(content.Substring(0, 100));
        }
        Console.WriteLine();
    }

    static void AsynchronousPattern()
    {
        Console.WriteLine($"{nameof(AsynchronousPattern)}");
        WebRequest request = WebRequest.Create(url);
        IAsyncResult result = request.BeginGetResponse(ReadResponse, request);

        static void ReadResponse(IAsyncResult result)
        {
            Console.WriteLine($"{nameof(ReadResponse)}");
            var request = result.AsyncState as WebRequest;
            using (var response = request?.EndGetResponse(result))
            {
                if (response != null)
                {
                    Stream stream = response.GetResponseStream();
                    var reader = new StreamReader(stream);
                    string content = reader.ReadToEnd();
                    Console.WriteLine(content.Substring(0, 100));
                }
            }
            Console.WriteLine();
        }

        Console.ReadLine();
    }

    static void EventBasedAsyncPattern()
    {
        Console.WriteLine($"{nameof(EventBasedAsyncPattern)}");
        WebClient webClient = new WebClient();
        webClient.DownloadStringCompleted += (s, e) =>
        {
            Console.WriteLine(e.Result.Substring(0, 100));
        };
        webClient.DownloadStringAsync(new Uri(url));

        Console.ReadLine();
    }

    static async Task TaskBasedAsyncPattern()
    {
        Console.WriteLine($"{nameof(TaskBasedAsyncPattern)}");
        WebClient webclient = new WebClient();
        string content = await webclient.DownloadStringTaskAsync(new Uri(url));
        Console.WriteLine(content.Substring(0, 100));
        return;
    }

    static async Task<string> TaskBasedAsyncPattern2()
    {
        Console.WriteLine($"{nameof(TaskBasedAsyncPattern2)}");
        WebClient webclient = new WebClient();
        string content = await webclient.DownloadStringTaskAsync(new Uri(url));
        return content.Substring(0, 100);
    }
}