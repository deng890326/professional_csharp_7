internal class Program
{
    private static void Main(string[] args)
    {
        //TaskUsingThreadPool();
        ParentAndChildTask();
        Console.ReadLine();
    }

    public static void TaskUsingThreadPool()
    {
        static void TaskMethod(object? o) => Log(o?.ToString() ?? "null");

        TaskFactory taskFactory = new TaskFactory();
        Task t1 = taskFactory.StartNew(TaskMethod, "using a task factory");
        Task t2 = Task.Factory.StartNew(TaskMethod, "using static property Task.Factory");
        Task t3 = new Task(TaskMethod, "using a Task ctor and Start");
        t3.Start();
        Task t4 = Task.Run(() => TaskMethod("using the Run method"));
    }

    private static object _lock = new object();

    public static void Log(string title)
    {
        //lock (typeof(Program))
        lock (_lock)
        {
            Console.WriteLine(title);
            Console.WriteLine($"Task id: {Task.CurrentId}, thred: {Thread.CurrentThread.ManagedThreadId}\n" +
                $"is pooled thread: {Thread.CurrentThread.IsThreadPoolThread}\n" +
                $"is background thread: {Thread.CurrentThread.IsBackground}");
            Console.WriteLine();
        }
    }

    public static void ParentAndChildTask()
    {
        Task parent = new Task(ParentTask);
        parent.Start();
        Thread.Sleep(2000);
        Console.WriteLine($"parent.Status={parent.Status}");
        Thread.Sleep(4000);
        Console.WriteLine($"parent.Status={parent.Status}");
    }

    private static void ParentTask()
    {
        Console.WriteLine("parent start");
        //Thread.Sleep(1000);
        Task child = new Task(ChildTask, TaskCreationOptions.AttachedToParent);
        child.Start();
        Thread.Sleep(1000);
        Console.WriteLine("parent end");
    }

    private static void ChildTask()
    {
        Console.WriteLine("child start");
        Thread.Sleep(5000);
        Console.WriteLine("child end");
    }
}