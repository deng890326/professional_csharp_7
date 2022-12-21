using BooksODataService.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BooksODataServiceHost.Controllers
{
    public class BookChaptersController : ODataController
    {
        [EnableQuery(MaxTop = int.MaxValue)]
        public IQueryable<BookChapter> Get() =>
            _context.Chapters.Include(c => c.Book);

        [EnableNestedPaths]
        [EnableQuery]
        public SingleResult<BookChapter> Get([FromODataUri] int key) =>
            SingleResult.Create(_context.Chapters.Where(c => c.Id == key));

        public BookChaptersController(BooksContext context) =>
            _context = context;

        private readonly BooksContext _context;
    }
}
