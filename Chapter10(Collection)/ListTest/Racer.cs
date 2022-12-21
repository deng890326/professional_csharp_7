using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListTest
{
    internal class Racer : IComparable<Racer?>, IComparable, IEquatable<Racer?>, IFormattable
    {
        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Country { get; }
        public int Wins { get; }

        public Racer(int id, string firstName, string lastName, string country, int wins)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Country = country;
            Wins = wins;
        }

        public Racer(int id, string firstName, string lastName, string country)
            : this(id, firstName, lastName, country, 0) { }

        public override string ToString() => $"{FirstName} {LastName}";

        public int CompareTo(Racer? other)
        {
            if (other == null) return -1;

            int cmp = FirstName.CompareTo(other?.FirstName);
            if (cmp != 0) return cmp;

            cmp = LastName.CompareTo(other?.LastName);
            if (cmp != 0) return cmp;

            cmp = Country.CompareTo(other?.Country);
            if (cmp != 0) return cmp;

            cmp = Wins.CompareTo(other?.Wins);
            return cmp;
        }

        public int CompareTo(object? obj)
        {
            return CompareTo(obj as Racer);
        }

        public bool Equals(Racer? other)
        {
            if (other == null) return false;

            return Id == other.Id && FirstName == other.FirstName && LastName == other.LastName
                && Country == other.Country && Wins == other.Wins;
        }

        public string ToString(string? format, IFormatProvider? formatProvider)
        {
            switch (format?.ToUpper())
            {
                case "N":
                    return $"{FirstName} {LastName}";
                case "F":
                    return FirstName;
                case "L":
                    return LastName;
                case "W":
                    return $"{FirstName} {LastName}, Wins: {Wins}";
                case "C":
                    return $"{FirstName} {LastName}, Country: {Country}";
                case "A":
                case null:
                    return $"{FirstName} {LastName}, Country: {Country}, Wins: {Wins}";
                default:
                    throw new FormatException(string.Format(formatProvider, $"Format {format} is not supported"));

            }
        }
    }
}
