namespace Property
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(new Test().Name);
            Console.WriteLine(new Test2().Name);
            Console.WriteLine();

            Person person = new Person(firstName: "Deng", lastName: "YingWei");
            Console.WriteLine(person.FullName);

            var captain = new
            {
                person.FirstName,
                person.LastName,
                Age = 30,
            };
            Console.WriteLine($"captain={captain}");
            Console.WriteLine(captain.GetType());
            Console.WriteLine();

            var doctor = new
            {
                person.FirstName,
                person.LastName,
                Age = 31,
            };
            Console.WriteLine($"doctor={doctor}");
            Console.WriteLine(doctor.GetType());
            Console.WriteLine($"doctor.GetType() == captain.GetType() ? {doctor.GetType() == captain.GetType()}");
            Console.WriteLine();

            var doctor1 = new
            {
                FirstName1 = person.FirstName,
                person.LastName,
                Age = 32,
            };
            Console.WriteLine($"doctor1={doctor1}");
            Console.WriteLine(doctor1.GetType());
            Console.WriteLine($"doctor1.GetType() == captain.GetType() ? {doctor1.GetType() == captain.GetType()}");
            Console.WriteLine();
        }
    }
    
    struct Test
    {
        private readonly string _name = "Test._name init in field";

        public Test()
        {
            _name = "Test._name init in construtor";
        }

        public string Name { get { return _name; } } 
    }

    struct Test2
    {
        public Test2()
        {
            Name = "Test2.Name set in constructor";
        }

        public string Name { get; } = "Test2.Name init in property";
    }

    readonly struct Person
    {
        public Person(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; }
        public string LastName { get; }

        public string FullName => $"{FirstName} {LastName}";
    }
}