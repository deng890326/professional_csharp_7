using BooksLib.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksLib.Repos
{
    public class BooksRepository : IBooksRepository
    {
        public async Task<Book?> AddAsync(Book item)
        {
            int id = 1;
            if (_books.Count > 0)
            {
                id = _books.Last().BookId + 1;
            }

            await Task.Delay(_simulatedDelay);

            Book bookToAdd = new Book(id, item);
            _books.Add(bookToAdd);
            return bookToAdd;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Book? bookToDelete = _books.FirstOrDefault(x => x.BookId == id);
            if (bookToDelete == null)
                return false;

            await Task.Delay(_simulatedDelay);

            return _books.Remove(bookToDelete);
        }

        public async Task<Book?> GetItemAsync(int id)
        {
            await Task.Delay(_simulatedDelay);
            return _books.FirstOrDefault(x => x.BookId == id);
        }

        public async Task<IEnumerable<Book>> GetItemsAsync()
        {
            await Task.Delay(_simulatedDelay);
            return _books;
        }

        public async Task<Book?> UpdateAsync(Book item)
        {
            Book? bookToUpdate = _books.FirstOrDefault(x => x.BookId == item.BookId);
            if (bookToUpdate == null)
                return null;

            await Task.Delay(_simulatedDelay);

            item.CopyTo(bookToUpdate);
            return bookToUpdate;
        }

        private readonly List<Book> _books = new List<Book>()
        {
            new Book(1) { Title = "Professional C# 7 and .NET Core 2", Publisher = "Wrox Press" },
            new Book(2) { Title = "Professional C# 6 and .NET Core 1.0", Publisher = "Wrox Press" },
            new Book(3) { Title = "Professional C# 5.0 and .NET 4.5.1", Publisher = "Wrox Press" },
            new Book(4) { Title = "Enterprise Services with the .NET Framework", Publisher = "AWL" }
        };

        private const int _simulatedDelay = 1000;
    }
}
