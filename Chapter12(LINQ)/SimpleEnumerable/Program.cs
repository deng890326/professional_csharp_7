using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SimpleEnumerable
{
    class Program
    {
        static void Main(string[] args)
        {
            Enumerable1 enumerable = new Enumerable1();
            foreach (var i in enumerable)
            {
                Console.WriteLine(i);
            }
            Console.WriteLine(); 
            Console.WriteLine(enumerable.LastOrDefault());
        }
    }

    class Enumerable1 : IEnumerable<long>
    {
        public IEnumerator<long> GetEnumerator()
        {
            return new Enumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class Enumerator : IEnumerator<long>
        {
            private long Previous { get; set; } = 0;

            public long Current { get; private set; } = 1;

            object IEnumerator.Current => throw new NotImplementedException();

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                long nextCurrent;
                checked
                {
                    try
                    {
                        nextCurrent = Current + Previous;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        return false;
                    }
                }
                Previous = Current;
                Current = nextCurrent;
                return true;
            }

            public void Reset()
            {
                Previous = 0;
                Current = 1;
            }
        }
    }
}
