using BooksLib.Models;

namespace BooksLib.Repos
{
    public interface IBooksRepository : IQueryRepository<Book, int>, IUpdateRepository<Book, int>
    {
    }
}
