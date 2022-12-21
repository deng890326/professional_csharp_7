using BooksServiceClientSample.Models;

namespace BooksServiceClientSample.Services
{
    public class BookChapterClientService : HttpClientService<BookChapter>
    {
        public BookChapterClientService(
            ILogger<HttpClientService<BookChapter>> logger)
            : base(logger)
        {
        }

        public async Task<IEnumerable<BookChapter>> GetAllAsync()
        {
            var chapters = await base.GetAllAsync(UrlService.BooksApi);
            return chapters.OrderBy(b => b.Number);
        }

        public Task<BookChapter?> GetAsync(Guid id)
        {
            return base.GetAsync(UrlService.BooksApi, id);
        }

        public Task<BookChapter?> Post(BookChapter item)
        {
            return base.Post(UrlService.BooksApi, item);
        }

        public Task Put(Guid id, BookChapter item)
        {
            return base.Put(UrlService.BooksApi, id, item);
        }

        public Task Delete(Guid id)
        {
            return base.Delete(UrlService.BooksApi, id);
        }
    }
}
