namespace OperatorTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int x = 1;
            uint y = 3000000000;
            var z = x + y;
            var z1 = y + x;
            Console.WriteLine($"typeof(z)={z.GetType().Name}, z={z}, typeof(z1)={z1.GetType().Name}, z1={z1}");
            //var f = new Test().Fun;
            Console.WriteLine(new Test().Fun);
            Console.WriteLine();

            Vector v1, v2, v3;
            v1 = new Vector(1.0, 1.5, 2.0);
            v2 = new Vector(0.0, 0.0, -10.0);
            v3 = v1 + v2;
            Console.WriteLine($"v1 = {v1}");
            Console.WriteLine($"v2 = {v2}");
            Console.WriteLine($"v3= v1 + v2 = {v3}");
            Console.WriteLine($"2 * v3 = {2 * v3})");
            Console.WriteLine($"v3 += v2 gives {v3 += v2}");
            Console.WriteLine($"v3 = v1 * 2 gives {v3 = v1 * 2}");
            Console.WriteLine($"v1 * v3 = {v1 * v3}");
            Console.WriteLine();

            Vector v4 = new Vector(1.0, 1.0, 1.0);
            Vector v5 = new Vector(1.0, 1.0, 1.0);
            Vector v6 = new Vector(2.0, 1.0, 1.0);
            Vector? v7 = v5, v8 = v6;
            Console.WriteLine($"v4.Equals(v5) ? {v4.Equals(v5)}");
            Console.WriteLine($"v4.Equals(v6) ? {v4.Equals(v6)}");
            Console.WriteLine($"v4.Equals(v7) ? {v4.Equals(v7)}");
            Console.WriteLine($"v4.Equals(v8) ? {v4.Equals(v8)}");
            Console.WriteLine($"v4.Equals(null) ? {v4.Equals(null)}");
            Console.WriteLine($"v4.Equals(0) ? {v4.Equals(0)}");
            Console.WriteLine();

        }

        class Test
        {
            //public int Fun(int x) => 1;
            public int Fun(int x, int y) => 2;
        }
    }
}