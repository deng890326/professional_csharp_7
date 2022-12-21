using BookServices.Models;

namespace BookServices.Services
{
    public class SampleChapters
    {
        public SampleChapters(IBookChaptersService service) =>
            _service = service;

        public void CreateSampleChapters()
        {
            IEnumerable<BookChapter> chapters =
                from i in Enumerable.Range(0, sampleTitles.Length)
                select new BookChapter()
                {
                    Title = sampleTitles[i],
                    Number = chapterNumbers[i],
                    Pages = chapterPages[i],
                };
            _service.AddRange(chapters);
        }

        private static readonly string[] sampleTitles = new[]
        {
            ".NET Application Architectures",
            "Core C#",
            "Objects and Types",
            "Object-Oriented Programming with C#",
            "Generics",
            "Operators and Casts",
            "Arrays",
            "Delegates, Lamadas, and Events",
            "Windows Communication Foundation"
        };

        private static readonly int[] chapterNumbers =
            { 1, 2, 3, 4, 5, 6, 7, 8, 9, 44 };

        private static readonly int[] chapterPages =
            {35, 42, 33, 20, 24, 38, 20, 32, 44};

        private readonly IBookChaptersService _service;
    }
}
