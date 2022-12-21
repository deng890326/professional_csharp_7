namespace EventSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UseManualResetEventSlim();
            //lambdaTest();
        }

        private static void lambdaTest()
        {
            string s = "string one";
            Console.WriteLine(s);
            Action action = () => s = "string two";
            Action action1 = () => Console.WriteLine($"lambda: {s}");
            Action action2 = static () => Console.WriteLine("static");
            Action action3 = () => Console.WriteLine("non static");
            action();
            action1();
            action2();
            action3();
            Console.WriteLine(s);
        }

        static void UseManualResetEventSlim()
        {
            const int size = 4;
            Calculator[] calculators = new Calculator[size];
            WaitHandle[] waitHandles = new WaitHandle[size];
            ManualResetEventSlim[] manualResetEvents = new ManualResetEventSlim[size];

            for (int i = 0; i < size; i++)
            {
                manualResetEvents[i] = new ManualResetEventSlim(false);
                waitHandles[i] = manualResetEvents[i].WaitHandle;
                calculators[i] = new Calculator(manualResetEvents[i]);
                Task.Factory.StartNew(i => calculators[i].Calculate(i, i), i);
            }

            for (int i = 0; i < size; i++)
            {
                int index = WaitHandle.WaitAny(waitHandles, 3000);
                if (index == WaitHandle.WaitTimeout)
                {
                    Console.WriteLine("Timeout!");
                    break;
                }
                else
                {
                    manualResetEvents[index].Reset();
                    Console.WriteLine($"finish task for {index}, result: {calculators[index].Result}");
                }
            }
        }
    }

    public static class TaskExtensions
    {
        public static Task StartNew<T>(this TaskFactory taskFactory, Action<T> action, T param)
        {
            Action<object?> action1 = o =>
            {
                T param = (T)o!;
                action(param);
            };

            return taskFactory.StartNew(action1, param);
        }
    }

    public class Calculator
    {
        public int? Result { get; private set; }

        public Calculator(ManualResetEventSlim manualResetEvent)
        {
            this.manualResetEvent = manualResetEvent;
            Result = null;
        }

        public void Calculate(int x, int y)
        {
            Console.WriteLine($"task {Task.CurrentId} start calculate {x} {y}");
            Thread.Sleep(2000);
            Result = x + y;
            Console.WriteLine($"task {Task.CurrentId} is completed");
            manualResetEvent.Set();
        }

        private ManualResetEventSlim manualResetEvent;
    }
}