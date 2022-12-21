using System.Numerics;

namespace QuickArray
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int sz = random.Next(int.MaxValue >> 12);
            Console.WriteLine($"sz={sz}");
            unsafe
            {
                int* p = stackalloc int[sz];
                for (int i = 0; i < sz; i++)
                {
                    p[i] = random.Next(sz);
                }
                BigInteger sum = new BigInteger(0);
                for (int i = 0; i < sz; i++)
                {
                    Console.WriteLine($"element at {i}: {p[i]}");
                    sum += p[i];
                    Console.WriteLine($"sum = {sum}");
                }
            }
        }
    }
}