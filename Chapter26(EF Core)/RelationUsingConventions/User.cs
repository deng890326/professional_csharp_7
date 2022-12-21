namespace RelationUsingConventions
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public List<Book>? AuthoredBooks { get; set; }

        public User(string name) => Name = name;

        public static implicit operator User(string name) => new(name);
    }
}