namespace OverflowTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int b = int.MaxValue;
            //checked
            {
                Console.WriteLine("b > b+1 ? {0}", b > b + 1);
            }
            unsafe
            {
                Console.WriteLine(sizeof(Point));
                int x = 1, y = 1;
                int* p = &x;
                int* p1 = &y;
                Console.WriteLine("p-p1={0}", p - p1);
                *p = 2;
                *(p + 1) = 2;
                Console.WriteLine("x={0}, y={1}", x, y);
            }
        }

        struct Point
        {
            public int x;
            public int y;
        }
    }
}