using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    public static class StringArrayExtensions
    {
        public static void ToStrings(this string[] values, out string value1, out string value2)
        {
            if (values.Length < 2) throw new ArgumentException(nameof(values) + " length not fit.");
            value1 = values[0];
            value2 = values[1];
        }
    }
}
