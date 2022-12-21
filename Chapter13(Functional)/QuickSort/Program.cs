using Extensions;
using System.Security.Cryptography;

namespace QuickSort
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] array = new int[128];
            Random r = new Random();

            foreach (int i in Enumerable.Range(0, 10))
            {
                Console.WriteLine($"test {i + 1}:");
                fillArrayAndSort(array, r);
                Console.WriteLine($"array.IsSorted() ? {array.IsSorted()}");
                Console.WriteLine();
            }
            
            static void Print(in int[] array)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    Console.Write($"{array[i],2}");
                    if ((i + 1) % 16 != 0)
                    {
                        Console.Write("  ");
                    }
                    else
                    {
                        Console.WriteLine();
                    }
                }
            }

            static void fillArrayAndSort(int[] array, Random r)
            {
                array.Foreach((int i, ref int x) => x = r.Next(99));
                Print(array);
                Console.WriteLine();
                QuickSort(array);
                Console.WriteLine($"{nameof(QuickSort)} result:");
                Print(array);
            }
        }

        static void QuickSort<T>(T[] elements) where T : IComparable<T>
        {
            sort(0, elements.Length - 1);

            void sort(int start, int end)
            {
                if (start >= end) return;

                int i = start, j = end + 1;
                T pivot = elements[start];
                while (true)
                {
                    while (elements[++i].CompareTo(pivot) < 0) if (i == end) break;
                    while (elements[--j].CompareTo(pivot) > 0) if (j == start) break;

                    if (i >= j) break;
                    swap(elements, i, j);
                }
                swap(elements, start, j);
                //Console.WriteLine($"i == j ? {i == j}, i = {i}, j = {j}");
                sort(start, j - 1);
                sort(j + 1, end);

                static void swap<T>(T[] elements, int i, int j)
                {
                    T temp = elements[i];
                    elements[i] = elements[j];
                    elements[j] = temp;
                }
            }
        }
    }
}