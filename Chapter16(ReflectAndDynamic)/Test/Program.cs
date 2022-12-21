using System.Reflection;
using VectorClass;

namespace Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Vector v = new Vector(100, 200, 300);
            foreach (var item in v)
            {
                Console.WriteLine(item);
            }

            Type type = typeof(Type);
            foreach (var mem in type.GetMembers(BindingFlags.Public | BindingFlags.Instance))
            {
                Console.WriteLine($"{mem.DeclaringType} {mem.MemberType} {mem.Name}");
            }
        }
    }
}