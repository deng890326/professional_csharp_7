using System.Collections;
using WhatsNewAttributes;

[assembly: SupportsWhatsNew]

namespace VectorClass
{

    [LastModified("19 Jul 2017", "updated for C# 7 and .NET Core 2.")]
    [LastModified("6 Jun 2015", "updated for C# 6 and .NET Core")]
    [LastModified("14 Dec 2010", "IEnumrable interface implemented: Vector can be treated as a collection.")]
    [LastModified("10 Feb 2010", "IFormattable interface implemented: Vector accepts N and VE format specifiers.")]
    public class Vector : IFormattable, IEnumerable<double>
    {
        private readonly double x;
        private readonly double y;
        private readonly double z;

        public double X => x;
        public double Y => y;
        public double Z => z;

        public Vector(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        [LastModified("19 Jul 2017", "Reduced the number of code lines.")]
        public Vector(Vector other) : this(other.X, other.Y, other.Z) { }

        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<double> GetEnumerator() => new VectorEnumerator(this);
        IEnumerator IEnumerable.GetEnumerator() => new VectorEnumerator(this);

        [LastModified("6 Jun 2015", "Changed to implement IEnumerable<T>.")]
        [LastModified("14 Feb 2010", "Class created as part of collection support for Vector.")]
        private class VectorEnumerator : IEnumerator<double>
        {
            //private readonly Vector vector;
            private int index = -1;
            private readonly unsafe double*[] dimensions;

            public unsafe VectorEnumerator(Vector v)
            {
                fixed (double* x = &v.x, y = &v.y, z = &v.z)
                {
                    dimensions = new double*[] { x, y, z };
                }
                //dimensions[2] = dimensions[2] + int.MaxValue;
            }

            public double Current
            {
                get
                {
                    if (index < dimensions.Length && index >= 0)
                        unsafe
                        {
                            return *dimensions[index];
                        }
                    else
                        throw new IndexOutOfRangeException($"index={index}, total={dimensions.Length}");
                }
            }

            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                if (++index < dimensions.Length)
                {
                    return true;
                }
                return false;
            }

            public void Reset()
            {
                index = -1;
            }
        }
    }
}