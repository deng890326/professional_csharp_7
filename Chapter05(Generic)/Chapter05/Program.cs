using Chapter05.List;
using Chapter05.List.Generic;
using System.Collections;

namespace Chapter05
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IMyList myList = new LinkedList();
            Console.WriteLine($"First: {myList.First ?? "null"}");
            Console.WriteLine($"Last: {myList.Last ?? "null"}");
            show(myList);
            Console.WriteLine();

            myList.AddLast(null);
            Console.WriteLine($"First: {myList.First ?? "null"}");
            Console.WriteLine($"Last: {myList.Last ?? "null"}");
            show(myList);
            Console.WriteLine();

            myList.AddLast(10);
            Console.WriteLine($"First: {myList.First ?? "null"}");
            Console.WriteLine($"Last: {myList.Last ?? "null"}");
            show(myList);
            Console.WriteLine();

            myList.AddLast(20);
            Console.WriteLine($"First: {myList.First ?? "null"}");
            Console.WriteLine($"Last: {myList.Last ?? "null"}");
            show(myList);
            Console.WriteLine();


            Console.WriteLine("Generic int:");
            IMyList<int> myGenericIntList = new List.Generic.LinkedList<int>();
            Console.WriteLine($"First: {myGenericIntList.First}");
            Console.WriteLine($"Last: {myGenericIntList.Last}");
            show(myGenericIntList);
            Console.WriteLine();

            myGenericIntList.AddLast(10);
            Console.WriteLine($"First: {myGenericIntList.First}");
            Console.WriteLine($"Last: {myGenericIntList.Last}");
            show(myGenericIntList);
            Console.WriteLine();

            myGenericIntList.AddLast(20);
            Console.WriteLine($"First: {myGenericIntList.First}");
            Console.WriteLine($"Last: {myGenericIntList.Last}");
            show(myGenericIntList);
            Console.WriteLine();


            Console.WriteLine("Generic string:");
            IMyList<string> myGenericStrList = new List.Generic.LinkedList<string>();
            Console.WriteLine($"First: {myGenericStrList.First}");
            Console.WriteLine($"Last: {myGenericStrList.Last}");
            show(myGenericStrList);
            Console.WriteLine();

            myGenericStrList.AddLast("string1");
            Console.WriteLine($"First: {myGenericStrList.First}");
            Console.WriteLine($"Last: {myGenericStrList.Last}");
            show(myGenericStrList);
            Console.WriteLine();

            myGenericStrList.AddLast("string2");
            Console.WriteLine($"First: {myGenericStrList.First}");
            Console.WriteLine($"Last: {myGenericStrList.Last}");
            show(myGenericStrList);
            Console.WriteLine();
        }

        static void show(IEnumerable values)
        {
            bool hasValue = false;
            foreach (var value in values)
            {
                hasValue = true;
                Console.Write((value ?? "null") + " ");
            }
            if (!hasValue)
            {
                Console.Write("Empty values");
            }
            Console.WriteLine();
        }
    }
}