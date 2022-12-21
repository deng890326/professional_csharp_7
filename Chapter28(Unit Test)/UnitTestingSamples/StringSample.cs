using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestingSamples
{
    public class StringSample
    {
        public StringSample(string init)
        {
            if (init is null)
                throw new ArgumentNullException(nameof(init));
            this.init = init;
        }

        public string GetStringDemo(string first, string second)
        {
            if (first is null)
                throw new ArgumentNullException(nameof(first));
            if (first is "")
                throw new ArgumentException(
                    "empty string is not allowed", nameof(first));

            if (second is null)
                throw new ArgumentNullException(nameof(second));
            if (second.Length > first.Length)
                throw new ArgumentOutOfRangeException(nameof(second),
                    $"{nameof(second)} must be shorter than {nameof(first)}");

            int ix = first.IndexOf(second);
            if (ix < 0)
            {
                return $"{second} not found in {first}";
            }
            else if (ix < 5)
            {
                string ret = first.Remove(ix, second.Length);
                return $"removed {second} from {first}: {ret}";
            }
            else
            {
                return init.ToUpperInvariant();
            }
        }

        private string init;
    }
}
