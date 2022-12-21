using System.Text.Json.Serialization;

namespace BooksServiceClientSample.Models
{
    public class BookChapter
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Guid? Id { get; set; }
        public int? Number { get; set; }
        public string? Title { get; set; }
        public int? Pages { get; set; }
    }
}
