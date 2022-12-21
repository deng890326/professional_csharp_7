using System.Globalization;

namespace NumberAndDateFormatting
{
    internal class Program
    {
        static void Main(string[] args)
        {
            NumberFormatDemo();
            //DateFormatDemo();
        }

        public static void NumberFormatDemo()
        {
            int val = 1234567890;
            Console.WriteLine(val.ToString("N"));
            foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
            {
                Console.WriteLine($"{culture,-15}: {val.ToString("N", culture)}");
            }
            //Console.WriteLine(val.ToString("N", new CultureInfo("fr-FR")));
            //Console.WriteLine(val.ToString("N", new CultureInfo("de-DE")));
            CultureInfo.CurrentCulture = new CultureInfo("zh-TW");
            Console.WriteLine($"{CultureInfo.CurrentCulture}: {val:N}");
        }

        public static void DateFormatDemo()
        {
            DateTime val = new(2022, 11, 23);
            Console.WriteLine(val.ToLongDateString());
            foreach (var culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
            {
                Console.WriteLine($"{culture,-15}: {val.ToString("D", culture)}");
            }
            //Console.WriteLine(val.ToString("D", new CultureInfo("fr-FR")));
            //Console.WriteLine(val.ToString("D", new CultureInfo("de-DE")));
            Console.WriteLine($"{CultureInfo.CurrentCulture}: {val:D}");
            CultureInfo.CurrentCulture = new CultureInfo("zh-TW");
            Console.WriteLine($"{CultureInfo.CurrentCulture}: {val:D}");
        }
    }
}