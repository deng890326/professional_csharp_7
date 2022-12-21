using Microsoft.EntityFrameworkCore;

namespace RelationUsingAnnotations
{
    [PrimaryKey(nameof(ChapterId))]
    public class Chapter
    {
        public int ChapterId { get; }
        public int Number { get; set; }
        public string Title { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}