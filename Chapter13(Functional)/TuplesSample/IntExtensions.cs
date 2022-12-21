using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuplesSample
{
    public static class IntExtensions
    {

        public static (int result, int remainder) DivideBy(this int dividend, int divisor)
        {
            return (dividend / divisor, dividend % divisor);
        }

        public static void Deconstruct(this int @this, out long square, out long cube)
        {
            square = @this * @this;
            cube = square * @this;
        }
    }
}
