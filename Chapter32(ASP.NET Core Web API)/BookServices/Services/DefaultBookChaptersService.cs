using BookServices.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace BookServices.Services
{
    public class DefaultBookChaptersService : IBookChaptersService
    {
        public Task Add(BookChapter bookChapter) => Task.Run(() =>
        {
            bookChapter.Id = Guid.NewGuid();
            _chapters[bookChapter.Id] = bookChapter;
        });

        public Task AddRange(IEnumerable<BookChapter> bookChapters) => Task.Run(() =>
        {
            foreach (BookChapter chapter in bookChapters)
            {
                Add(chapter);
            }
        });

        public Task<BookChapter?> Find(Guid id) => Task.Run(() =>
        {
            _chapters.TryGetValue(id, out BookChapter? bookChapter);
            return bookChapter;
        });

        public Task<IEnumerable<BookChapter>> GetAll() => Task.Run(() =>
        {
            return (IEnumerable<BookChapter>)_chapters.Values;
        });

        public Task<BookChapter?> Remove(Guid id) => Task.Run(() =>
        {
            _chapters.TryRemove(id, out BookChapter? chapter);
            return chapter;
        });

        public Task Update(BookChapter bookChapter) => Task.Run(() =>
        {
            if (_chapters.ContainsKey(bookChapter.Id))
            {
                BookChapter current = _chapters[bookChapter.Id];
                if (bookChapter.Title != null && bookChapter.Title.Any())
                {
                    current.Title = bookChapter.Title;
                }
                if (bookChapter.Pages != null)
                {
                    current.Pages = bookChapter.Pages;
                }
                if (bookChapter.Number != null)
                {
                    current.Number = bookChapter.Number;
                }
            }
        });

        private readonly ConcurrentDictionary<Guid, BookChapter> _chapters =
            new ConcurrentDictionary<Guid, BookChapter>();
    }
}
