namespace SpanTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] arr = { 1, 4, 5, 11, 13, 19 };
            var span = new Span<int>(arr);
            span[1] = 101;
            Console.WriteLine(string.Join(',', arr));

            var span2 = span.Slice(1);
            span2[1] = 102;
            Console.WriteLine(string.Join(',', arr));

            var span3 = new Span<int>(arr, 2, 4);
            span3[1] = 103;
            Console.WriteLine(string.Join(',', arr));
        }
    }
}