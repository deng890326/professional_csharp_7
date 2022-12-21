using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Where1<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            foreach (var item in source)
            {
                if (predicate(item))
                    yield return item;
            }
        }

        public static IEnumerable<T> Where2<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return new WhereIterator<T>(source, predicate);
        }

        private class WhereIterator<T> : IEnumerable<T>
        {
            public WhereIterator(in IEnumerable<T> source, in Func<T, bool> predicate)
            {
                _source = source;
                _predicate = predicate;
            }

            public IEnumerator<T> GetEnumerator()
            {
                foreach (var item in _source)
                {
                    if (_predicate(item))
                        yield return item;
                }
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            private readonly IEnumerable<T> _source;
            private readonly Func<T, bool> _predicate;
        }

        public static IEnumerable<T> Where3<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return iterator();

            IEnumerable<T> iterator()
            {
                foreach (var item in source)
                {
                    if (predicate(item))
                        yield return item;
                }
            }
        }
    }
}
