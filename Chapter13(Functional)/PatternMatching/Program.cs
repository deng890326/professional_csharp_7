namespace PatternMatching
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var p1 = new Person("Katharina", "Nagel");
            var p2 = new Person("Matthias", "Nagel");
            var p3 = new Person("Stephanie", "Nagel");
            object?[] data = { null, 42, 25, "a string", p1, p2, new Person[1] { p3 } };
            foreach (var item in data)
            {
                SwtichStatement(item);
            }
        }

        static void SwtichStatement(object? item)
        {
            switch (item)
            {
                case null: // is null
                case 42: // is 42
                    Console.WriteLine($"It's a const pattern with: {item}.");
                    break;
                case int i: // is int i
                    Console.WriteLine($"It's a type pattern with int: {i}");
                    break;
                case string s: // is string s
                    Console.WriteLine($"It's a type pattern with string: {s}");
                    break;
                case Person p when p.FirstName.StartsWith('K'): // is Person p && p.FirstName.StartsWith('K')
                    Console.WriteLine($"It's a type pattern with Person and when clause: {p}");
                    break;
                case Person p: // is Person p
                    Console.WriteLine($"It's a type pattern with Person: {p}");
                    break;
                case var every: // is var every
                    Console.WriteLine($"It's a var pattern with: {every}");
                    break;
                default:
            }
        }
    }

    class Person
    {
        public string FirstName { get; }
        public string LastName { get; }

        public Person(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public override string ToString() => $"{FirstName} {LastName}";

        public void Deconstruct(out string firstName, out string lastName)
        {
            firstName = FirstName;
            lastName = LastName;
        }
    }
}