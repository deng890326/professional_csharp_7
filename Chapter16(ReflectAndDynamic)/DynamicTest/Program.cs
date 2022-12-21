namespace DynamicTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            dynamic dyn = 100;
            PrintDyn(dyn);
            dyn = "This is a string";
            PrintDyn(dyn);
            dyn = (FirstName: "Bugs", LastName: "Bunny");
            PrintDyn(dyn);

            static void PrintDyn(dynamic dyn)
            {
                Console.WriteLine(dyn.GetType());
                Console.WriteLine(dyn);
                Console.WriteLine();
            }

            UsingStatic();
            UsingDynamic();
        }

        static void UsingStatic()
        {
            StaticClass staticObj = new StaticClass();
            Console.WriteLine(staticObj.IntValue);
        }

        static void UsingDynamic()
        {
            DynamicClass dynamicObj = new DynamicClass();
            Console.WriteLine(dynamicObj.DynValue);
        }

        class StaticClass
        {
            public int IntValue = 100;
        }

        class DynamicClass
        {
            public dynamic DynValue = 100;
        }
    }
}