using System.Collections;

namespace StructuralEquatableTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var janet = new Person("Janet", "Jackson");
            Person[] people =
            {
                new Person("Michael", "Jackson"),
                janet
            };
            Person[] people1 =
            {
                new Person("Michael", "Jackson"),
                janet
            };

            Console.WriteLine($"people == people1 ? {people == people1}");
            Console.Write("people as IStructuralEquatable).Equals(people1, EqualityComparer<Person>.Default: ");
            Console.WriteLine((people as IStructuralEquatable).Equals(people1, EqualityComparer<Person>.Default));
        }
    }

    class Person : IEquatable<Person?>
    {
        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get; }

        public Person(string firstName, string lastName)
        {
            Id = 0;
            FirstName = firstName;
            LastName = lastName;
        }   

        public static bool operator ==(in Person? left, in Person? right)
        {
            if (ReferenceEquals(left, right)) return true;
            return left?.Id == right?.Id 
                && left?.FirstName == right?.FirstName
                && left?.LastName == right?.LastName;
        }

        public static bool operator !=(in Person? left, in Person? right) => !(left == right);

        public bool Equals(Person? other)
        {
            if (other == null) return false;
            return this == other;
        }

        public override bool Equals(object? obj)
        {
            Person? other = obj as Person;
            return Equals(other);
        }
    }
}