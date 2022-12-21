using BookServices.Models;

namespace BookServices.Services
{
    public interface IBookChaptersService
    {
        Task Add(BookChapter bookChapter);
        Task AddRange(IEnumerable<BookChapter> bookChapters);
        Task<IEnumerable<BookChapter>> GetAll();
        Task<BookChapter?> Find(Guid id);
        Task<BookChapter?> Remove(Guid id);
        Task Update(BookChapter bookChapter);
    }
}
