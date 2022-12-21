using Extensions;
using static Extensions.FunctionExtensions;

namespace ExpressionBodiedMembers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person("Deng Yingwei");
            person.FirstName += '\'';
            Console.WriteLine(person);

            var greetPerson = Compose(f1: (string name) => new Person(name),
                                      f2: (Person p) => $"Hello, {p.FirstName}");
            Console.WriteLine(greetPerson("Chen Lu"));
        }
    }

    class Person
    {
        public Person(string name) => name.Split(' ').ToStrings(out firstName, out lastName);

        public ref string FirstName => ref firstName;
        public ref string LastName => ref lastName;

        public override string ToString() => $"{FirstName} {LastName}";

        private string firstName;
        private string lastName;
    }
}