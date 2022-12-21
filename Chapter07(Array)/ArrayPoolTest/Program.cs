using System.Buffers;

namespace ArrayPoolTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ArrayPool<int> arrayPool = ArrayPool<int>.Create();
            Console.WriteLine("ArrayPool<int>.Create():");
            Console.WriteLine($"arrayPool.GetType()={arrayPool.GetType()}");
            useArrayPool(arrayPool);
            Console.WriteLine();

            ArrayPool<int> arrayPool2 = ArrayPool<int>.Shared;
            Console.WriteLine("ArrayPool<int>.Shared:");
            Console.WriteLine($"arrayPool2.GetType()={arrayPool2.GetType()}");
            useArrayPool(arrayPool2);
            Console.WriteLine();

            // 实际上从数组池里租用的数组可以超过8k
            Console.WriteLine($"8 << 10 = {8 << 10}");
            ArrayPool<int> arrayPool3 = ArrayPool<int>.Create(maxArrayLength: 8 << 10, maxArraysPerBucket: 10);
            Console.WriteLine("ArrayPool<int>.Create(maxArrayLength: 8 << 10, maxArraysPerBucket: 10):");
            Console.WriteLine($"arrayPool3.GetType()={arrayPool3.GetType()}");
            useArrayPool(arrayPool3);
            Console.WriteLine();
        }

        private static void useArrayPool(ArrayPool<int> arrayPool)
        {
            for (int i = 1; i < 11; i++)
            {
                int minLength = i << 10;
                int[] array = arrayPool.Rent(minLength);
                Console.WriteLine($"minLength={minLength}, array.Length={array.Length}");
            }
        }
    }
}