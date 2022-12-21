using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    public static class FunctionExtensions
    {
        public static void Use<T>(this T @this, Action<T> action) where T : IDisposable
        {
            using (@this) action(@this);
        }

        public static Func<T1, TResult> Compose<T1, T2, TResult>(
            Func<T1, T2> f1, Func<T2, TResult> f2) => x => f2(f1(x));
    }
}
