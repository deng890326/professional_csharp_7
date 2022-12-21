using System.IO;
using System.IO.Pipes;
using System.Text;

namespace PipesReader
{
    internal class Program
    {
        private const byte EOT = 4;

        static void Main(string[] args)
        {
            PipesReader("server");
        }

        private static void PipesReader(string pipeName)
        {
            try
            {
                using NamedPipeServerStream pipeStream = new(pipeName, PipeDirection.In);
                Console.WriteLine("server waiting for connection...");
                pipeStream.WaitForConnection();
                Console.WriteLine("server connected.");

                byte[] buffer = new byte[128];
                while (true)
                {
                    int bytesRead = pipeStream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                        continue;

                    if (buffer[bytesRead - 1] == EOT)
                    {
                        Console.WriteLine(Encoding.UTF8.GetString(buffer, 0, bytesRead - 1));
                        break;
                    }
                    else
                    {
                        Console.WriteLine(Encoding.UTF8.GetString(buffer, 0, bytesRead));
                    }
                }

                Console.WriteLine("exit");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(PipesReader)} {ex}");
            }
        }
    }
}