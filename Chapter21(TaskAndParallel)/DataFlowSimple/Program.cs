using System.Threading.Tasks.Dataflow;

namespace DataFlowSimple
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //ActionBlock();
            ProducerAndComsumer();
        }

        static void ActionBlock()
        {
            ActionBlock<string> processInput = new ActionBlock<string>(s =>
            Console.WriteLine($"user input: {s}"));

            string? input;
            while ((input = Console.ReadLine()) != null
                && string.Compare(input, "exit", ignoreCase: true) != 0)
            {
                processInput.Post(input);
            }
        }

        static BufferBlock<string> buffer = new BufferBlock<string>();

        static void Producer()
        {
            string? input;
            while ((input = Console.ReadLine()) != null
                && string.Compare(input, "exit", ignoreCase: true) != 0)
            {
                buffer.Post(input);
            }
            buffer.Post("exit");
        }

        static void Comsumer()
        {
            string? data;
            while ((data = buffer.Receive()) != null
                && string.Compare(data, "exit", ignoreCase: true) != 0)
            {
                Console.WriteLine($"user input: {data}");
            }
        }

        static void ProducerAndComsumer()
        {
            Task producer = Task.Run(Producer);
            Task comsumer = Task.Run(Comsumer);
            Task.WaitAll(producer, comsumer);
        }
    }
}