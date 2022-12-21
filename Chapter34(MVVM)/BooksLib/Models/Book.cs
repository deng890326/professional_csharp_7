using Framewok;
using System;

namespace BooksLib.Models
{
    public class Book : BindableBase, ICloneable, ICopyOperation<Book>
    {
        public Book(int id = 0) => BookId= id;

        public Book(int id, Book other) : this(id)
        {
            other.CopyTo(this);
        }

        public int BookId { get; } = 0;

        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        public string Publisher
        {
            get => _publisher;
            set => Set(ref _publisher, value);
        }

        private string _title = "";
        private string _publisher = "";

        public void CopyTo(Book book)
        {
            book.Title = _title;
            book.Publisher = _publisher;
        }

        public object Clone()
        {
            return new Book(BookId, this);
        }
    }
}
