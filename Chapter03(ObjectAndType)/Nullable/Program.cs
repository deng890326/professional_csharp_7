namespace Nullable
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int? x1 = null;
            int? x2 = 1;
            int x3 = x1.HasValue ? x1.Value : -1;
            int x4 = x2 ?? -1;
            Console.WriteLine($"x3={x3}, x4={x4}");
        }
    }
}