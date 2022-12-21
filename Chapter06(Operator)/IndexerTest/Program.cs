namespace IndexerTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Person p0 = new Person("Ayrton", "Senna", new DateTime(1960, 3, 21));
            Person p1 = new Person("Ronnie", "Peterson", new DateTime(1944, 2, 14));
            Person p2 = new Person("Jochen", "Rindt", new DateTime(1942, 2, 25));
            PersonCollection personCollection = new PersonCollection(p0, p1, p2);
            Print(personCollection);
            Console.WriteLine();

            personCollection[0] = new Person("Deng", "Yingwei", new DateTime(1989, 3, 26));
            Console.WriteLine("After Change Index 0:");
            Print(personCollection);
            Console.WriteLine();

            ref Person pIn1 = ref personCollection[1];
            Console.WriteLine($"pIn1.GetType() = {pIn1.GetType()}");
            Console.WriteLine($"p1.GetType() = {p1.GetType()}");
            Console.WriteLine($"ReferenceEquals(pIn1, p1) = {ReferenceEquals(pIn1, p1)}");
            pIn1 = new Person("Chen", "Lu", new DateTime(1990, 12, 26));
            Console.WriteLine("After Change Index 1:");
            Print(personCollection);
            Console.WriteLine();

        }

        static void Print(PersonCollection personCollection)
        {
            for (int i = 0; i < personCollection.Count; i++)
            {
                Console.WriteLine($"person {i}: ({personCollection[i]}");
            }
        }
    }

    class Person
    {
        public DateTime Birthday { get; }
        public string FirstName { get; }
        public string LastName { get; }

        public Person(string firstName, string lastName, DateTime birthday)
        {
            Birthday = birthday;
            FirstName = firstName;
            LastName = lastName;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}, Birthday: {Birthday}";
        }
    }

    class PersonCollection
    {
        private Person[] people;
        public PersonCollection(params Person[] people)
        {
            this.people = people.ToArray();
        }

        public ref Person this[int index]
        {
            get => ref people[index];
        }

        public int Count => people.Length;

        //public Person this[int index]
        //{
        //    get => people[index];
        //    set => people[index] = value;
        //}
    }
}