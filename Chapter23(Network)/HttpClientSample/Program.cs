using System.Net;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System;

namespace HttpClientSample
{
    internal class Program
    {
        //private const string url = "http://services.data.org/Northwind/Northwind.svc/Regions";
        //private const string wrongUrl = "http://services.data.org/Northwind1/Northwind.svc/Regions";
        private const string url = "https://api.thecatapi.com/v1/images/search?limit=1";
        private const string url2 = "http://api.thecatapi.com/v1/images/search?limit=1";
        private const string google = "http://www.google.com";
        private const string baidu = "http://www.baidu.com";

        static void Main(string[] args)
        {
            WebClientAsync().Wait();
            Console.WriteLine();

            GetDataSimpleAsync().Wait();
            Console.WriteLine();

            GetDataAsync().Wait();
            Console.WriteLine();

            GetDataUsingTcpAsync().Wait();
        }

        private static async Task WebClientAsync()
        {
            using (WebClient client = new WebClient())
            {
                string result = await client.DownloadStringTaskAsync(new Uri(url));
                Console.WriteLine(result);
            }
        }

        private static async Task GetDataSimpleAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage? response = await client.GetAsync(url);
                if (response != null && response.IsSuccessStatusCode)
                {
                    Console.WriteLine(response.ToString());
                    //Console.WriteLine($"{(int)response.StatusCode} {response.ReasonPhrase}");
                    //Console.WriteLine(await response.Content.ReadAsStringAsync());
                }
                else
                {
                    Console.WriteLine($"response={response}");
                }
            }
        }

        private static async Task GetDataAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpRequestMessage request =
                    new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("Accept", "application/json");
                Console.WriteLine($"request: {request}");
                ShowHeaders("DefaultRequestHeaders", client.DefaultRequestHeaders);
                ShowHeaders("request.Headers", request.Headers);

                HttpResponseMessage response = await client.SendAsync(request);
                if (response != null && response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"{(int)response.StatusCode} {response.ReasonPhrase}");
                    ShowHeaders("response.Headers", response.Headers);
                    ShowHeaders("response.Content.Headers", response.Content.Headers);
                    Console.WriteLine(await response.Content.ReadAsStringAsync());
                }
            }
        }

        private static void ShowHeaders(string title, HttpHeaders headers)
        {
            Console.WriteLine($"{title}:");
            foreach (var header in headers)
            {
                string value = string.Join("; ", header.Value);
                Console.WriteLine($"{header.Key}: {value}");
            }
        }

        private static async Task GetDataUsingTcpAsync()
        {
            const int readBufferSize = 1024;
            try
            {
                using TcpClient client = new TcpClient();
                Uri uri = new Uri(url2);
                await client.ConnectAsync(uri.Host, uri.Port);
                using NetworkStream stream = client.GetStream();
                string header = new StringBuilder()
                    .AppendLine($"GET {uri.PathAndQuery} HTTP/1.1")
                    .AppendLine($"Host: {uri.Host}:{uri.Port}")
                    .AppendLine("method: GET")
                    .AppendLine($"path: {uri.PathAndQuery}")
                    .AppendLine($"scheme: {uri.Scheme}")
                    .AppendLine("Accept: application/json")
                    .AppendLine("Connection: close")
                    .AppendLine()
                    .ToString();
                Console.WriteLine("TcpClient sending:");
                Console.WriteLine(header);
                byte[] buffer = Encoding.UTF8.GetBytes(header);
                await stream.WriteAsync(buffer, 0, buffer.Length);
                await stream.FlushAsync();

                using MemoryStream ms = new MemoryStream();
                //await stream.CopyToAsync(ms, readBufferSize);
                buffer = new byte[readBufferSize];
                int nRead = 0;
                do
                {
                    nRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                    await ms.WriteAsync(buffer.AsMemory(0, nRead));
                    Array.Clear(buffer, 0, buffer.Length);
                } while (nRead > 0);
                ms.Seek(0, SeekOrigin.Begin);
                using StreamReader streamReader = new StreamReader(ms);
                Console.WriteLine("TcpClient received:");
                Console.WriteLine(await streamReader.ReadToEndAsync());
                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(GetDataUsingTcpAsync)} {ex}");
            }
        }
    }
}