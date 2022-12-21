namespace SimpleLINQ
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var names = new List<string>()
            {
                "Nino", "Alberto", "Juan", "Mike", "Phil"
            };
            var namesWithJ = from n in names
                             where n.StartsWith("J")
                             orderby n
                             select n;

            Console.WriteLine("First iteration:");
            foreach (string name in namesWithJ)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine();

            names.Add("John");
            names.Add("Jim");
            names.Add("Jack");
            names.Add("Denny");
            Console.WriteLine("Second iteration:");
            foreach (string name in namesWithJ)
            {
                Console.WriteLine(name);
            }
        }
    }
}