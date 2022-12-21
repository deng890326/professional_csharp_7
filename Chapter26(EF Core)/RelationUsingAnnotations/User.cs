using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace RelationUsingAnnotations
{
    [PrimaryKey(nameof(UserId))]
    public class User
    {
        public int UserId { get; }
        public string Name { get; set; }

        [InverseProperty("Author")]
        public List<Book>? AuthoredBooks { get; set; }

        [InverseProperty("Reviewer")]
        public List<Book>? ReviewedBooks { get; set; }

        [InverseProperty("ProjectEditor")]
        public List<Book>? EditedBooks { get; set; }

        public User(string name) => Name = name;

        public static implicit operator User(string name) => new(name);

        public override string ToString() => Name;
    }
}