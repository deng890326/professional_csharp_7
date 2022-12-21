namespace GenericStaticMember
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StaticDemo<int>.x = 5;
            StaticDemo<double>.x = 6;
            StaticDemo<string>.x = 7;
            StaticDemo<StaticDemo<string>>.x = 8;
            StaticDemo<Program>.x = 9;

            Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}",
                StaticDemo<int>.x, StaticDemo<double>.x, 
                StaticDemo<string>.x, StaticDemo<StaticDemo<string>>.x,
                StaticDemo<decimal>.x, StaticDemo<Program>.x);
        }
    }

    class StaticDemo<T>
    {
        public static int x;
    }
}