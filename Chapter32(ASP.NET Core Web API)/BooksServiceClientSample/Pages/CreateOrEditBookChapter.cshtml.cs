using BooksServiceClientSample.Models;
using BooksServiceClientSample.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static BooksServiceClientSample.NameHelper;

namespace BooksServiceClientSample.Pages
{
    public class CreateOrEditBookChapterModel : PageModel
    {
        public async Task<IActionResult> OnGetAsync([FromQuery] bool edit)
        {
            if (IsCreate)
            {
                IsEdit = true;
            }
            else
            {
                BookChapter = await _service.GetAsync(Id) ?? BookChapter;
                if (BookChapter.Id == null || BookChapter.Id == Guid.Empty)
                {
                    return NotFound(Id);
                }
                IsEdit = edit;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync([FromQuery] bool delete)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (IsCreate)
            {
                await _service.Post(BookChapter);
            }
            else
            {
                if (delete)
                {
                    await _service.Delete(Id);
                }
                else
                {
                    BookChapter.Id = Id;
                    await _service.Put(Id, BookChapter);
                }
            }

            return RedirectToPage(PageNameOf<Pages_Index>());
        }

        public bool IsCreate => Id == Guid.Empty;

        public bool IsEdit { get; set; }

        [BindProperty(SupportsGet = true)]
        [FromQuery]
        public Guid Id { get; set; } = Guid.Empty;

        [BindProperty(SupportsGet = false)]
        [FromForm]
        public BookChapter BookChapter { get; set; } = new();

        public CreateOrEditBookChapterModel(
            BookChapterClientService service) =>
            _service = service;

        private readonly BookChapterClientService _service;
    }
}
