using MyStructLib;
using System.Linq;
using System.Runtime.InteropServices;

namespace SpanSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SpanOnTheHeap();
            SpanOnTheStack();
            SpanOnNativeMemory();
            SpanExtensions();
        }

        const int size = 10;

        static void SpanOnTheHeap()
        {
            int[] arr = new int[size];
            Span<int> span = new Span<int>(arr); //Span<int> span = arr.AsSpan();
            span.Slice(start: 3, length: 4).Fill(42);
            Console.WriteLine(string.Join(" ", arr));
            Console.WriteLine();
        }

        static unsafe void SpanOnTheStack()
        {
            MyStruct* ptr = stackalloc MyStruct[size];
            Span<MyStruct> span = new Span<MyStruct>(ptr, size);
            span.Slice(start: 3, length: 4).Fill(new MyStruct { n = 42 });
            Console.WriteLine(string.Join(' ', span.ToArray()));
            Console.WriteLine();
        }

        static void SpanOnNativeMemory()
        {
            IntPtr p = Marshal.AllocHGlobal(size * sizeof(int));
            IntPtr p2 = new IntPtr();
            try
            {
                unsafe
                {
                    Console.WriteLine(p.ToString("X"));

                    int* ptr = (int*)p.ToPointer();
                    Span<int> span = new Span<int>(ptr, size);
                    span.Fill(42);
                    Console.WriteLine(string.Join(' ', span.ToArray()));

                    //p2 = Marshal.AllocHGlobal(-1);
                    p2 = Marshal.AllocHGlobal(0);
                    Console.WriteLine(p2.ToString("X"));
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                Marshal.FreeHGlobal(p);
                Marshal.FreeHGlobal(p2);
                Console.WriteLine();
            }
        }

        static void SpanExtensions()
        {
            var arr = new int[]{
                1, 5, 11, 25, 36, 46, 57, 68, 74, 83, 94
            };
            Span<int> span1 = new Span<int>(arr);
            Span<int> span2 = span1.Slice(3, 4);
            bool overlaps = span1.Overlaps(span2);
            Console.WriteLine($"overlaps={overlaps}");
            overlaps = span2.Overlaps(span1);
            Console.WriteLine($"overlaps={overlaps}");
            Console.WriteLine();

            int index = span1.IndexOf(span2);
            Console.WriteLine($"index={index}");
            index = span1.IndexOfAny(span2);
            Console.WriteLine($"index={index}");
            Console.WriteLine();

            Console.WriteLine("Before Reverse:");
            Console.WriteLine($"arr=[{string.Join(',', arr)}]");
            Console.WriteLine($"arr=[{string.Join(',', span2.ToArray())}]");
            span1.Reverse();
            Console.WriteLine("After Reverse:");
            Console.WriteLine($"arr=[{string.Join(',', arr)}]");
            Console.WriteLine($"arr=[{string.Join(',', span2.ToArray())}]");
            Console.WriteLine();
        }
    }
}