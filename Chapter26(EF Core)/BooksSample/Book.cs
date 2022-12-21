using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksSample
{
    public class Book
    {
        private Book() { }

        public Book(string title, string publisher) {
            _title= title;
            _publisher = publisher;
        }
        public string Title => _title;
        public string Publisher => _publisher;

        public override string ToString()
        {
            return $"id: {_bookId}, title: {_title}, publisher: {_publisher}";
        }

        private int _bookId = 0;
        private string _title;
        private string _publisher;
    }
}
