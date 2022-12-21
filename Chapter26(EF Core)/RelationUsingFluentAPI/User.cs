using System.ComponentModel.DataAnnotations.Schema;

namespace RelationUsingFluentAPI
{
    public class User
    {
        public int UserId { get; }
        public string Name { get; set; }

        public List<Book>? AuthoredBooks { get; set; }

        public List<Book>? ReviewedBooks { get; set; }

        public List<Book>? EditedBooks { get; set; }

        public User(string name) => Name = name;

        public static implicit operator User(string name) => new(name);

        public override string ToString() => Name;
    }
}