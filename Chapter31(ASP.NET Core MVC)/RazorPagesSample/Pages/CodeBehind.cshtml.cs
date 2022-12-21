using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesSample.Models;

namespace RazorPagesSample.Pages
{
    public class CodeBehindModel : PageModel
    {
        public CodeBehindModel(BooksContext context) =>
                    _context = context;

        public IEnumerable<Book> Books = Enumerable.Empty<Book>();

        [BindProperty()]
        public Book EditingBook { get; set; } = new();

        public string Message { get; set; } = "";

        public async Task OnPost()
        {
            if (Check(EditingBook))
            {
                try
                {
                    await _context.Books.AddAsync(EditingBook);
                    await _context.SaveChangesAsync();
                    Message = "Book saved";
                    EditingBook = new();
                }
                catch (Exception ex)
                {
                    Message = $"Save error: {ex.Message}";
                    throw;
                }
            }

            Books = await _context.Books.ToListAsync();
        }

        public async Task OnGet()
        {
            bool created = await _context.Database.EnsureCreatedAsync();
            if (created) await SeedBooks();
            Books = await _context.Books.ToListAsync();
            Message = "==== Add New Book Below ====";
        }

        private async Task SeedBooks()
        {
            await _context.Books.AddRangeAsync(new Book[]
            {
            new()
            {
                Title = "Professional C# 7 and .NET Core 2",
                Publisher = "Wrox Press"
            },
            new()
            {
                Title = "Professional C# 10 and .NET Core 6",
                Publisher = "Wrox Press"
            }
            });
            await _context.SaveChangesAsync();
        }

        private bool Check(Book book)
        {
            if (book.Publisher.Count() > 10)
            {
                Message = "Publisher too long.";
                return false;
            }
            return true;
        }

        private BooksContext _context;
    }
}
