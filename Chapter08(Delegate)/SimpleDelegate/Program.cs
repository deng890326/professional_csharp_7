namespace SimpleDelegate
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread t = new Thread(() =>
            {
                delegateTest();
            });
            t.Name = "Thread One";
            t.Start();

            Console.WriteLine($"in Main thread: \nMain() finished\n");
        }

        static void delegateTest()
        {
            int x = 1;
            GetAString firstStringMethod = () =>
            {
                //ref int refx = ref x;
                //return refx.ToString();
                return x.ToString();
            };
            Console.WriteLine($"in {Thread.CurrentThread.Name} thread: ");
            Console.WriteLine($"firstStringMethod.GetType()={firstStringMethod.GetType()}");
            Console.WriteLine($"typeof(GetAString).BaseType={typeof(GetAString).BaseType}");
            x++;
            Console.WriteLine($"firstStringMethod()={firstStringMethod()}\n");

            Timer timer = new Timer(state =>
            {
                Console.WriteLine($"in {Thread.CurrentThread.Name} thread: ");
                Console.WriteLine($"state={state}");
                x++;
                Console.WriteLine($"in timer, firstStringMethod()={firstStringMethod()}\n");
            });
            timer.Change(0, 1000);

            Thread t = new Thread(() =>
            {
                Console.WriteLine($"in {Thread.CurrentThread.Name} thread: ");
                x++;
                Console.WriteLine($"in new thread, firstStringMethod()={firstStringMethod()}\n");
            });
            t.Name = "Thread Two";
            t.Start();

            Console.WriteLine($"in {Thread.CurrentThread.Name} thread: \ndelegateTest() finished\n");
        }

        delegate string GetAString();
    }
}