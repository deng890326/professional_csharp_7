using BooksLib.Models;
using BooksLib.Repos;
using Framewok.Services;
using Microsoft.Extensions.Options;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace BooksLib.Services
{
    public class BooksService : IItemsService<Book>
    {
        public class Options
        {
            public bool AutoRefresh;
        }

        public BooksService(IBooksRepository booksRepository, IOptions<Options> options)
        {
            _repo = booksRepository;
            AutoRefresh = options.Value.AutoRefresh;
            if (AutoRefresh)
            {
                _ = RefreshAsync();
            }
        }

        public bool AutoRefresh { get; }

        public ObservableCollection<Book> Items => _books;

        public Book? SelectedItem {
            get => _selectedBook;
            set
            {
                _selectedBook = value;
                SelectedItemChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler? SelectedItemChanged;

        public async Task<bool> RefreshAsync()
        {
            var newBooks = await _repo.GetItemsAsync();
            Items.Clear();
            foreach (var book in newBooks)
            {
                Items.Add(book);
            }
            SelectedItem = Items.FirstOrDefault();
            return true;
        }

        public async Task<Book?> AddOrUpdateAsync(Book item)
        {
            bool existing = item.BookId != 0;
            Book? ret = null;
            if (existing)
            {
                Book? updated = await _repo.UpdateAsync(item);
                if (updated != null && AutoRefresh)
                {
                    await RefreshAsync();
                    ret = Items.FirstOrDefault(x => x.BookId == updated.BookId);
                }
                else
                {
                    ret = updated;
                }
            }
            else
            {
                Book? added = await _repo.AddAsync(item);
                if (added != null && AutoRefresh)
                {
                    await RefreshAsync();
                    ret = Items.FirstOrDefault(x => x.BookId == added.BookId);
                }
                else
                {
                    ret = added;
                }
            }

            return ret;
        }

        public async Task<bool> RemoveAsync(Book item)
        {
            bool ret = await _repo.DeleteAsync(item.BookId);
            if (ret == true && AutoRefresh)
            {
                return await RefreshAsync();
            }
            else
            {
                return ret;
            }
        }

        private readonly IBooksRepository _repo;

        private readonly ObservableCollection<Book> _books =
            new ObservableCollection<Book>();

        private Book? _selectedBook = null; 
    }
}
