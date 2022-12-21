using Moq;

namespace MockingSamplesBooksLib.Tests
{
    public class BooksServiceTest : IDisposable
    {
        private const string TestTitle = "Test Title";
        private const string UpdatedTestTitle = "Updated Test Title";
        private const string APublisher = "A Publisher";
        private BooksService _booksService;
        private Book _newBook = new Book()
        {
            BookId = 0,
            Title = TestTitle,
            Publisher = APublisher,
        };
        private Book _addedBook = new Book()
        {
            BookId = 1,
            Title = TestTitle,
            Publisher = APublisher,
        };
        private Book _updatedBook = new Book()
        {
            BookId = 1,
            Title = UpdatedTestTitle,
            Publisher = APublisher,
        };
        private Book _notInRepoBook = new Book()
        {
            BookId = 42,
            Title = TestTitle,
            Publisher = APublisher,
        };

        public BooksServiceTest()
        {
            var mock = new Mock<IBooksRepository>();
            mock.Setup(repo => repo.AddAsync(_newBook)).ReturnsAsync(_addedBook);
            mock.Setup(repo => repo.UpdateAsync(_notInRepoBook)).ReturnsAsync(default(Book));
            mock.Setup(repo => repo.UpdateAsync(_updatedBook)).ReturnsAsync(_updatedBook);

            _booksService = new BooksService(mock.Object);
        }

        [Fact]
        public async Task AddOrUpdateAsync_ThrowsForNull()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(
                () => _booksService.AddOrUpdateAsync(null));
        }

        [Fact]
        public async Task AddOrUpdateAsync_UpdateNotExistingBookThrows()
        {
            await Assert.ThrowsAsync<InvalidOperationException>(
                () => _booksService.AddOrUpdateAsync(_notInRepoBook));
        }

        [Fact]
        public async Task AddOrUpdateAsync_NewBook()
        {
            Book? actualBook = await _booksService.AddOrUpdateAsync(_newBook);
            Assert.Equal(_addedBook, actualBook);
            Assert.Contains(_addedBook, _booksService.Books);
        }

        [Fact]
        public async Task AddOrUpdateAsync_ExistingBook()
        {
            await _booksService.AddOrUpdateAsync(_newBook);
            Book? actualBook = await _booksService.AddOrUpdateAsync(_updatedBook);
            Assert.Equal(_updatedBook, actualBook);
            Assert.Contains(_updatedBook, _booksService.Books);
        }

        public void Dispose()
        {
        }
    }
}