using System.Globalization;

namespace FormattableStringTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int x = 3, y = 4;
            FormattableString str = $"The result of {x} + {y} is {x + y}";
            Console.WriteLine($"str.GetType()={str.GetType()}");
            Console.WriteLine($"format: {str.Format}");
            foreach (var item in str.GetArguments())
            {
                Console.WriteLine(item);
            }
            Console.WriteLine();

            var day = new DateTime(2025, 2, 14);
            Console.WriteLine($"{day:d}");
            Console.WriteLine(((FormattableString)$"{day:d}").ToString(CultureInfo.InvariantCulture));
            Console.WriteLine(FormattableString.Invariant($"{day:d}"));
            Console.WriteLine();

            string s = "Hello";
            string formatString = $"{s}, {{0}}";
            Console.WriteLine(formatString, "World!");
            Console.WriteLine();

            double d = 123456.123456;
            Console.WriteLine($"{d:#######.###}");
            Console.WriteLine($"{d:0000000.000}");
            Console.WriteLine();

            Person person = new Person { FirstName = "Deng", LastName = "Yingwei" };
            Console.WriteLine($"{person}");
            Console.WriteLine(person.ToString("F"));
            Console.WriteLine($"{person:F}");
            try
            {
                Console.WriteLine(person.ToString("B"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            try
            {
                Console.WriteLine($"{person:C}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine();

            string text = "asdfaf Visual Studio";
            var spanToText = text.AsSpan();
            var slice = spanToText.Slice(text.IndexOf("Visual"), 13);
            Console.WriteLine(new string(slice));

            var span2ToText = text.AsSpan(text.IndexOf("Visual"), 12);
            Console.WriteLine(span2ToText.ToString());
        }

        class Person : IFormattable
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public override string ToString() => $"{FirstName} {LastName}";
            public string ToString(string? format) => ToString(format, null);

            public string ToString(string? format, IFormatProvider? formatProvider)
            {
                switch (format)
                {
                    case null:
                    case "A":
                        return ToString();
                    case "F":
                        return FirstName;
                    case "L":
                        return LastName;
                    default:
                        throw new FormatException($"invalid format string {format}");
                }
            }
        }
    }
}