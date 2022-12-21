namespace BooksODataService.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Isbn { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }
        public List<BookChapter> Chapters { get; set; } = new();
    }
}
