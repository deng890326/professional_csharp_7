using BookServices.Models;
using BookServices.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BooksServiceSampleHost.Controllers
{
    [Produces("application/json", "application/xml")]
    [Route("api/[controller]", Name = RouteName)]
    [ApiController]
    public class BookChaptersController : ControllerBase
    {
        private const string RouteName = "BookChapters";
        // GET: api/<BookChaptersController>
        [HttpGet]
        public Task<IEnumerable<BookChapter>> GetAsync()
        {
            return  _service.GetAll();
        }

        // GET api/<BookChaptersController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            BookChapter? bookChapter = await _service.Find(id);
            if (bookChapter == null)
            {
                return NotFound();
            }
            return new ObjectResult(bookChapter);
        }

        // POST api/<BookChaptersController>
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] BookChapter? value)
        {
            if (value == null)
            {
                return BadRequest();
            }
            await _service.Add(value);
            return CreatedAtRoute(RouteName, value.Id, value);
        }

        // PUT api/<BookChaptersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] BookChapter? value)
        {
            if (value == null || value.Id != id)
            {
                return BadRequest();
            }
            if (_service.Find(id) == null)
            {
                return NotFound();
            }
            await _service.Update(value);
            return NoContent();
        }

        // DELETE api/<BookChaptersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            if (_service.Find(id) == null)
            {
                return NotFound();
            }
            await _service.Remove(id);
            return NoContent();
        }

        public BookChaptersController(IBookChaptersService service) =>
            _service = service;

        private readonly IBookChaptersService _service;
    }
}
