namespace BubbleSorter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Employee[] employees =
            {
                new Employee("Bugs Bunny", 20000),
                new Employee("Elmer Fudd", 10000),
                new Employee("Daffy Duck", 25000),
                new Employee("Wile Coyote", 1000000.38m),
                new Employee("Foghorn Leghorn", 23000),
                new Employee("RoadRunner", 50000)
            };
            
            static void print(Employee[] employees)
            {
                foreach (Employee e in employees)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            print(employees);
            Console.WriteLine();

            BubbleSorter<Employee>.Sort(employees, Employee.compareSalary);

            Console.WriteLine("After Sort:");
            print(employees);
            Console.WriteLine();
        }
    }

    static class BubbleSorter<T>
    {
        public static void Sort(IList<T> list, Func<T, T, bool> comparison)
        {
            bool swapped;
            for (int sorted = 0; sorted < list.Count - 1; sorted++)
            {
                swapped = false;
                for (int i = 1; i < list.Count - sorted; i++)
                {
                    if (comparison(list[i], list[i-1]))
                    {
                        T temp = list[i];
                        list[i] = list[i-1];
                        list[i - 1] = temp;
                        swapped = true;
                    }
                }
                if (!swapped) break;
            }
        }
    }

    class Employee
    {
        public string Name { get; }
        public decimal Salary { get; }

        public Employee(string name, decimal salary)
        {
            Name = name;
            Salary = salary;
        }

        public override string ToString() => $"{Name} {Salary:C}";

        public static bool compareSalary(Employee x, Employee y) => x.Salary < y.Salary;
    }
}