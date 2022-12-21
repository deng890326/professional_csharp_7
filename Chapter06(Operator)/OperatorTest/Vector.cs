using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperatorTest
{
    internal struct Vector : IEquatable<Vector?>, IEquatable<Vector>
    {
        public double X;
        public double Y;
        public double Z;

        public Vector(in double x, in double y, in double z)
        {
            X = x; Y = y; Z = z;
        }

        public Vector(in Vector vector)
        {
            X = vector.X; Y = vector.Y; Z = vector.Z;
        }

        public override string ToString() => $"( {X}, {Y}, {Z} )";

        public bool Equals(Vector? other)
        {
            string message = $"bool Equals(Vector? other), other={other}";
            Debug.WriteLine(message);
            Console.WriteLine(message);
            if (other == null) return false;
            return Equals((Vector)other);
        }

        public bool Equals(Vector other)
        {
            string message = $"bool Equals(Vector other), other={other}";
            Debug.WriteLine(message);
            Console.WriteLine(message);
            return this == other;
        }

        public override bool Equals(object? obj)
        {
            string message = $"bool Equals(object? obj), obj={obj}";
            Debug.WriteLine(message);
            Console.WriteLine(message);
            Vector? other = obj as Vector?;
            return Equals(other);
        }

        public static Vector operator +(in Vector left, in Vector right)
        {
            return new Vector(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        }

        public static Vector operator *(in double left, in Vector right)
        {
            return new Vector(left * right.X, left * right.Y, left * right.Z);
        }

        public static Vector operator *(in Vector left, in double right) => right * left;

        public static double operator *(in Vector left, in Vector right)
        {
            return left.X * right.X + left.Y * right.Y + left.Z * right.Z;
        }

        public static bool operator ==(in Vector left, in Vector right)
        {
            //if (ReferenceEquals(left, right)) return true;
            return left.X == right.X && left.Y == right.Y && left.Z == right.Z;
        }

        public static bool operator !=(in Vector left, in Vector right) => !(left == right);

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
        }

    }
}
