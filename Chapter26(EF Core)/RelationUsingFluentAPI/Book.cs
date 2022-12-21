using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationUsingFluentAPI
{
    public class Book
    {
        public int BookId { get; }

        public string Title { get; set; }

        public List<Chapter> Chapters { get; } = new List<Chapter>(); // 通过Chapter的显式定义的外键BookId获取到

        public User? Author { get; set; }

        public User? Reviewer { get; set; }

        public User? ProjectEditor { get; set; }

        public Book(string title = "") => Title = title;

        public static implicit operator Book(string title) => new(title);

        public override string ToString() => Title;
    }
}
