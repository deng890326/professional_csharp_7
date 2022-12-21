using System.Globalization;
using System.Text;

namespace StreamSamples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Creating data...");
            CreateSampleFile().Wait();
            Console.WriteLine("Input number to seek:");
            RandomAccess();
        }

        private static void RandomAccess()
        {
            using (var inputStream = File.OpenRead(sampleFilePath))
            {
                string? s = null;
                while ((s = Console.ReadLine()) != null
                    && !string.Equals(s, "exit", StringComparison.OrdinalIgnoreCase))
                {
                    if (!int.TryParse(s, out int index))
                        continue;
                    inputStream.Seek(index * size, SeekOrigin.Begin);
                    byte[] buffer = new byte[size];
                    inputStream.Read(buffer, 0, size);
                    Console.WriteLine($"record:\n{Encoding.UTF8.GetString(buffer)}");
                }
            }
        }

        private const int nRecords = 1 << 24;
        private static int size = 0;

        private readonly static string sampleFilePath =
            Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            + Path.DirectorySeparatorChar + "samplefile.data";

        static async Task CreateSampleFile()
        {
            Random random = new Random();
            var records = Enumerable.Range(0, nRecords).Select(x => new
            {
                Number = x,
                Text = $"Sample text {random.Next(200)}",
                Date = DateTime.Now.AddDays(random.Next(365 * 2000)),
            });

            using (var writer = new StreamWriter(sampleFilePath))
            {
                foreach (var record in records)
                {
                    string date = record.Date.ToString("d", CultureInfo.InvariantCulture);
                    string s = $"#{record.Number,8};{record.Text,-20};{date}#{Environment.NewLine}";
                    if (size == 0) size = s.Length;
                    await writer.WriteAsync(s);
                }
            }
        }
    }
}