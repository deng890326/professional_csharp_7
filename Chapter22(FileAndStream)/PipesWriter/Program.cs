using System.IO.Pipes;
using System.Text;
using System.Text.Unicode;

namespace PipesWriter
{
    internal class Program
    {
        private const byte EOT = 4;

        static void Main(string[] args)
        {
            PipesWriter(".", "server");
        }

        private static void PipesWriter(string serverName, string pipeName)
        {
            try
            {
#if DEBUG
                Random r = new Random();
                var largeBuffer = Enumerable.Range(0, int.MaxValue >> 1)
                    .Select(x => (byte)(r.Next(26) + 'A')).ToArray()!;
#endif
                using NamedPipeClientStream pipeStream = new(serverName, pipeName, PipeDirection.Out);
                int timeout = 5000;
                while (true)
                {
                    Console.WriteLine("connecting to server...");
                    try
                    {
                        pipeStream.Connect(timeout);
                    }
                    catch (TimeoutException)
                    {
                        Console.WriteLine($"connect timeout after {timeout}.");
                    }
                    if (pipeStream.IsConnected) break;
                }
                Console.WriteLine($"client connected.");

#if DEBUG
                Console.Write("writing large buffer...");
                pipeStream.Write(largeBuffer, 0, largeBuffer.Length);
                pipeStream.Flush();
#endif
                while (true)
                {
                    string? input = Console.ReadLine();
                    if (input == null || input.Length == 0)
                        continue;

                    byte[] inputBuffer;
                    if (string.Compare(input, "exit", true) == 0)
                        inputBuffer = new byte[] { EOT };
                    else
                        inputBuffer = Encoding.UTF8.GetBytes(input);

                    if (inputBuffer[inputBuffer.Length - 1] == EOT)
                    {
                        pipeStream.Write(inputBuffer, 0, inputBuffer.Length);
                        pipeStream.Flush();
                        break;
                    }
                    else
                    {
                        pipeStream.WriteAsync(inputBuffer, 0, inputBuffer.Length);
                        pipeStream.FlushAsync();
                    }
                }

                Console.WriteLine("disconnecting...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(PipesWriter)} {ex}");
            }
        }
    }
}