using BooksODataService.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BooksODataServiceHost.Controllers
{
    public class BooksController : ODataController
    {
        [EnableQuery(MaxTop = int.MaxValue)]
        public IQueryable<Book> Get() =>
            _context.Books.Include(b => b.Chapters);

        [EnableNestedPaths]
        [EnableQuery]
        public SingleResult<Book> Get([FromODataUri] int key) =>
            SingleResult.Create(_context.Books.Where(b => b.Id == key));

        public BooksController(BooksContext context) =>
            _context = context;

        private readonly BooksContext _context;
    }
}
