namespace BooksODataService.Models
{
    public class BookChapter
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public string Title { get; set; }
        public int Number { get; set; }
    }
}