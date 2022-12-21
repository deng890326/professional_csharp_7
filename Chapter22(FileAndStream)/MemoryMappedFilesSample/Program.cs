using System.IO;
using System.IO.MemoryMappedFiles;

namespace MemoryMappedFilesSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //MemoryFileTest();
            MemoryFileStreamTest();
        }

        private static ManualResetEventSlim createdEvent = new ManualResetEventSlim(false);
        private static ManualResetEventSlim writtenEvent = new ManualResetEventSlim(false);
        private const string mapName = "SampleMap";
        private const int fileSize = 1000;

        private static void Writter()
        {
            try
            {
                using MemoryMappedFile mmFile = MemoryMappedFile.CreateOrOpen(mapName, fileSize);
                createdEvent.Set();
                Console.WriteLine("mm file created.");
                using MemoryMappedViewAccessor accessor =
                    mmFile.CreateViewAccessor(0, fileSize, MemoryMappedFileAccess.Write);
                Random r = new Random();
                for (int i = 0; i < fileSize / sizeof(int); i += sizeof(int))
                {
                    int value = r.Next();
                    accessor.Write(i, value);
                    Console.WriteLine($"written {value} at {i}");
                }
                writtenEvent.Set();
                Console.WriteLine("all data written");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(Writter)}: {ex}");
            }
        }

        
        private static void Reader()
        {
            try
            {
                Console.WriteLine("reader waiting created event");
                createdEvent.Wait();
                Console.WriteLine("reader start");
                using MemoryMappedFile mmFile = MemoryMappedFile.OpenExisting(mapName);
                using MemoryMappedViewAccessor accessor =
                    mmFile.CreateViewAccessor(0, fileSize, MemoryMappedFileAccess.Read);
                Console.WriteLine("reader waiting written event");
                writtenEvent.Wait();
                Console.WriteLine("reader start read");
                for (int i = 0; i < fileSize / sizeof(int); i += sizeof(int))
                {
                    accessor.Read(i, out int value);
                    Console.WriteLine($"reading {value,10} at {i}");
                }
                Console.WriteLine("reader completed");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(Reader)}: {ex}");
            }
        }

        private static async Task WritterUsingStreamAsync()
        {
            try
            {
                using MemoryMappedFile mmFile = MemoryMappedFile.CreateOrOpen(mapName, fileSize);
                createdEvent.Set();
                Console.WriteLine("mm file created.");
                using MemoryMappedViewStream stream =
                    mmFile.CreateViewStream(0, fileSize, MemoryMappedFileAccess.Write);
                using StreamWriter streamWritter = new StreamWriter(stream);
                //streamWritter.AutoFlush = true;
                Random r = new Random();
                int writtenSize = 0;
                int count = 0;
                while (true)
                {
                    string value = $"some data {r.Next(),10}";
                    int dataSize = (value.Length + 1) * sizeof(char);
                    if (writtenSize + dataSize > fileSize)
                        break;
                    Console.WriteLine($"{++count,2}: writting {value} at {writtenSize}");
                    await streamWritter.WriteLineAsync(value);
                    writtenSize += dataSize;
                }
                await stream.FlushAsync();
                Console.WriteLine($"writtenSize={writtenSize}, stream.Position={stream.Position}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(Writter)}: {ex}");
            }
            finally
            {
                writtenEvent.Set();
                Console.WriteLine("all data written");
            }
        }

        private static async Task ReaderUsingStreamAsync()
        {
            try
            {
                Console.WriteLine("reader waiting created event");
                createdEvent.Wait();
                Console.WriteLine("reader start");
                using MemoryMappedFile mmFile = MemoryMappedFile.OpenExisting(mapName);
                using MemoryMappedViewStream stream =
                    mmFile.CreateViewStream(0, fileSize, MemoryMappedFileAccess.Read);
                using StreamReader streamReader = new StreamReader(stream);
                Console.WriteLine("reader waiting written event");
                writtenEvent.Wait();
                Console.WriteLine("reader start read");
                int count = 0;
                while (streamReader.Peek() != 0)
                {
                    long pos = stream.Position;
                    string? data = await streamReader.ReadLineAsync();
                    if (data == null || data.Length == 0)
                        break;
                    Console.WriteLine($"{++count,2}: reading {data} at {pos}");
                }
                Console.WriteLine("reader completed");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(Reader)}: {ex}");
            }
        }

        private static void MemoryFileTest()
        {
            Task.Run(Writter);
            Task.Run(Reader);
            Console.WriteLine("tasks started.");
            Console.ReadLine();
        }

        private static void MemoryFileStreamTest()
        {
            Task.Run(WritterUsingStreamAsync);
            Task.Run(ReaderUsingStreamAsync);
            Console.WriteLine("tasks started.");
            Console.ReadLine();
        }
    }
}