using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationUsingConventions
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public List<Chapter> Chapters { get; } = new List<Chapter>(); // 通过Chapter的显式定义的外键BookId获取到
        public User? Author { get; set; } // 自动生成外键，并且在模型中定义阴影属性

        public Book(string title = "") => Title = title;

        public static implicit operator Book(string title) => new(title);

        public override string ToString() => Title;
    }
}
