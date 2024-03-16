using LMS3.Models;

namespace LMS3.Repositories
{
    public class BookRepository
    {
        public static List<Book> _bookList = new List<Book>()
        {
            new Book { Id = 1, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald" },
            new Book { Id = 2, Title = "To Kill a Mockingbird", Author = "Harper Lee" },
            new Book { Id = 3, Title = "1984", Author = "George Orwell" }
        };

        public static List<Book> GetAllBooks()
        {
            return _bookList;
        }

        public static Book? GetBookById(int id)
        {
            var book = _bookList.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                return book;
            }
            return null;
        }

        public static void AddBook(Book book)
        {
            var maxId = _bookList.Max(b => b.Id);
            book.Id = maxId + 1;
            _bookList.Add(book);
        }

        public static void UpdateBook(Book book)
        {
            var existingBook = _bookList.FirstOrDefault(b => b.Id == book.Id);
            if (existingBook != null)
            {
                existingBook.Title = book.Title;
                existingBook.Author = book.Author;
            }
        }

        public static void DeleteBook(int id)
        {
            var book = _bookList.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                _bookList.Remove(book);
            }
        }

        public static bool IsBookAvailable(int bookId)
        {
            var book = _bookList.FirstOrDefault(b => b.Id == bookId);
            return book != null && !book.IsAvailable;
        }

        public static void MarkBookAsBorrowed(int bookId)
        {
            var book = _bookList.FirstOrDefault(b => b.Id == bookId);
            if (book != null)
            {
                book.IsAvailable = true;
            }
        }

        public static void MarkBookAsReturned(int bookId)
        {
            var book = _bookList.FirstOrDefault(b => b.Id == bookId);
            if (book != null)
            {
                book.IsAvailable = false;
            }
        }
    }
}