using BooksServiceClientSample.Models;
using BooksServiceClientSample.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BooksServiceClientSample.Pages
{
    public class IndexModel : PageModel
    {
        public IndexModel(BookChapterClientService service, ILogger<IndexModel> logger)
        {
            _service = service;
            _logger = logger;
            BookChapters = Enumerable.Empty<BookChapter>();
        }

        public IEnumerable<BookChapter> BookChapters { get; set; }

        public async Task OnGet()
        {
            BookChapters = await _service.GetAllAsync();
        }

        private BookChapterClientService _service;
        private readonly ILogger<IndexModel> _logger;
    }
}