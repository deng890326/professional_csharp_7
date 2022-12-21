namespace TuplesSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            (string s, int i, Person p) t1 = ("magic", 42, new Person("Deng", "Yingwei"));
            (string s, int i, Person p) t2 = ("magic'", 43, new Person("Chen", "Lu"));
            (string tempS, int tempI, Person tempP) temp = t1;
            Console.WriteLine(temp);
            (string s1, int i1, Person p1) = t1;
            Console.WriteLine($"{s1} {i1} {p1}");
            (var s2, var i2, var p2) = t2;
            Console.WriteLine($"{s2} {i2} {p2}");
            (s2, i2, p2) = t1;
            Console.WriteLine($"{s2} {i2} {p2}");
            (_, _, p2) = t2;
            Console.WriteLine($"{s2} {i2} {p2}");
            (_, _, Person p3) = t1;
            Console.WriteLine($"{p3}");
            Console.WriteLine();

            (int result, int remainder) = 5.DivideBy(3);
            Console.WriteLine($"5.DivideBy(3), result: {result}, remainder: {remainder}");
            Console.WriteLine();

            Console.WriteLine($"{nameof(TuplesWithLinkedList)}:");
            TuplesWithLinkedList();
            Console.WriteLine();

            (var first, var last) = p3;
            Console.WriteLine($"{first} {last}");
            Console.WriteLine();

            Console.WriteLine("Deconstruct int with extension:");
            (long square, long cube) = 3;
            Console.WriteLine($"{square} {cube}");
            Console.WriteLine();
        }

        static void TuplesWithLinkedList()
        {
            var list = new LinkedList<(int, int)>(Enumerable.Range(0, 10).Select(i => (i, i * i)));

            LinkedListNode<(int, int)>? node = null;
            while ((node = next(list, node)) != null)
            {
                (int value1, int value2) = node.Value;
                Console.WriteLine($"{value1} {value2}");
            }

            static LinkedListNode<(int, int)>? next(in LinkedList<(int, int)> list, in LinkedListNode<(int, int)>? node) =>
                node == null ? list.First : node.Next;
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