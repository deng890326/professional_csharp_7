using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MockingSamplesBooksLib
{
    public interface IBooksRepository
    {
        Task<IEnumerable<Book>> GetItemsAsync();
        Task<Book?> AddAsync(Book book);
        Task<Book?> UpdateAsync(Book book);
    }
}
