using System.Collections.ObjectModel;

namespace MockingSamplesBooksLib
{
    public class BooksService
    {
        public BooksService(IBooksRepository booksRepository)
        {
            if (booksRepository == null)
                throw new ArgumentNullException(nameof(booksRepository));

            _booksReop = booksRepository;
            _books = new ObservableCollection<Book>();
        }

        public async Task LoadAsync()
        {
            if (_books.Count > 0) return;
            var books = await _booksReop.GetItemsAsync();
            foreach (var book in books)
            {
                _books.Add(book);
            }
        }

        public IEnumerable<Book> Books => _books;

        public Book? Get(int BookId) => (from b in _books
                                         where b.BookId == BookId
                                         select b).FirstOrDefault();

        public async Task<Book?> AddOrUpdateAsync(Book book)
        {
            if (book == null)
                throw new ArgumentNullException(nameof(book));

            Book? ret = null;
            if (book.BookId == 0)
            {
                ret = await _booksReop.AddAsync(book);
                if (ret != null)
                {
                    _books.Add(ret);
                }
            }
            else
            {
                ret = await _booksReop.UpdateAsync(book);
                if (ret != null)
                {
                    Book old = (from b in _books
                                where b.BookId == ret.BookId
                                select b).First();
                    int ix = _books.IndexOf(old);
                    _books.RemoveAt(ix);
                    _books.Insert(ix, book);
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }

            return ret;
        }

        private readonly ObservableCollection<Book> _books;
        private readonly IBooksRepository _booksReop;
    }
}