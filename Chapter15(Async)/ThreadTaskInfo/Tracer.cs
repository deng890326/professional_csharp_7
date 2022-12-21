namespace ThreadTaskInfo
{
    public class Tracer
    {
        public static void TraceThreadAndTask(string info)
        {
            Console.WriteLine(GetThreadAndTask(info));
        }

        public static string GetThreadAndTask(string info)
        {
            string taskInfo = $"task(id = {Task.CurrentId?.ToString() ?? "null"})";
            return $"{info} in thread(id={Thread.CurrentThread.ManagedThreadId} and {taskInfo}";
        }
    }
}