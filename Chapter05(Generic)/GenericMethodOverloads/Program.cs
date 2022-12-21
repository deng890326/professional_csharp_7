namespace GenericMethodOverloads
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Test test = new Test();
            test.Foo(1);
            test.Foo(1, "a");
            test.Foo("a", 1);
            test.Foo("a");
            test.Bar(1);
        }

        class Test
        {
            public void Foo<T>(T obj) =>
                Console.WriteLine($"Foo<T>(T obj), T={typeof(T).Name}, obj={obj}");

            public void Foo(int x) =>
                Console.WriteLine($"Foo(int x), x={x}");

            public void Foo<T1, T2>(T1 obj1, T2 obj2) =>
                Console.WriteLine($"Foo<T1, T2>(T1 obj1, T2 obj2), T1={typeof(T1).Name}, obj1={obj1}, T2={typeof(T2).Name}, obj2={obj2}");

            //public void Foo<T>(int x, T obj) =>
            //    Console.WriteLine($"Foo<T>(int x, T obj), x={x}, T={typeof(T).Name}, obj={obj}");

            public void Foo<T>(T obj, int x) =>
                Console.WriteLine($"Foo<T>(T obj, int x), T={typeof(T).Name}, obj={obj}, x={x}");

            public void Bar<T>(T obj) => Foo(obj);
        }
    }
}