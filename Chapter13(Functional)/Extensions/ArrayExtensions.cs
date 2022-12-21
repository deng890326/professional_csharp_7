using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    public static class ArrayExtensions
    {

        public delegate void Operation<T>(int i, ref T x);

        public static void Foreach<T>(this T[] values, Operation<T> operation)
        {
            for (int i = 0; i < values.Length; i++)
            {
                operation(i, ref values[i]);
            }
        }

        public static bool IsSorted<T>(this T[] values) where T : IComparable<T>
        {
            if (values == null) throw new ArgumentNullException(nameof(values));
            if (values.Length < 2) return true;

            T previous = values[0];
            foreach (T current in values.Skip(1))
            {
                if (current.CompareTo(previous) < 0) return false;
                previous = current;
            }

            return true;
        }
    }
}
