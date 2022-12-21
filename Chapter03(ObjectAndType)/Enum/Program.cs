namespace EnumTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DaysOfWeek daysOfWeek = DaysOfWeek.Monday | DaysOfWeek.Thursday;
            Console.Write("switch(daysOfWeek): ");
            switch (daysOfWeek)
            {
                case DaysOfWeek.Monday:
                    Console.WriteLine("Monday");
                    break;
                case DaysOfWeek.Tuesday:
                    Console.WriteLine("Tuesday");
                    break;
                case DaysOfWeek.Wednesday:
                    Console.WriteLine("Wednesday");
                    break;
                case DaysOfWeek.Thursday:
                    Console.WriteLine("Thursday");
                    break;
                case DaysOfWeek.Friday:
                    Console.WriteLine("Friday");
                    break;
                default:
                    Console.WriteLine("Weekend");
                    break;
            }
            Console.WriteLine();

            Console.Write("foreach: ");
            foreach (DaysOfWeek day in Enum.GetValues(typeof(DaysOfWeek)))
            {
                if (day == (daysOfWeek & day))
                {
                    Console.Write(day + ", ");
                }
            }
            Console.WriteLine();
        }
    }

    [Flags]
    enum DaysOfWeek
    {
        Monday = 1,
        Tuesday = 2,
        Wednesday = 4,
        Thursday = 8,
        Friday = 16,
        Saturday = 32,
        Sunday = 64,
    }
}