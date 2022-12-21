using System;
using System.Collections.Generic;
using System.Text;

namespace DataLib
{
    public static class Enumerables
    {
        public static IEnumerable<(int key1, long value1)> Items1
        {
            get
            {
                yield return (1, 100);
                yield return (1, 101);
                yield return (2, 200);
                yield return (3, 300);
            }
        }

        public static IEnumerable<(int key2, string value2)> Items2
        {
            get
            {
                yield return (1, "One");
                yield return (1, "One'");
                yield return (3, "Three");
            }
        }
    }
}
