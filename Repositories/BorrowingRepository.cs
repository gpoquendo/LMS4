using LMS3.Models;

namespace LMS3.Repositories
{
    public class BorrowingRepository
    {
        public static List<Borrowing> _borrowingList = new List<Borrowing>()
        {
            new Borrowing { Id = 1, ReaderId = 1, BookId = 1, BorrowDate = new DateTime(2021, 1, 1), ReturnDate = new DateTime(2021, 1, 15) },
            new Borrowing { Id = 2, ReaderId = 2, BookId = 2, BorrowDate = new DateTime(2021, 2, 1), ReturnDate = new DateTime(2021, 2, 15) },
            new Borrowing { Id = 3, ReaderId = 3, BookId = 3, BorrowDate = new DateTime(2021, 3, 1), ReturnDate = new DateTime(2021, 3, 15) }
        };

        public static List<Borrowing> GetAllBorrowings()
        {
            return _borrowingList;
        }

        public static Borrowing? GetBorrowingById(int id)
        {
            var borrowing = _borrowingList.FirstOrDefault(b => b.Id == id);
            if (borrowing != null)
            {
                return borrowing;
            }
            return null;
        }

        public static void AddBorrowing(Borrowing borrowing)
        {
            if (!BookRepository.IsBookAvailable(borrowing.BookId))
            {
                throw new Exception("Book is not available for borrowing");
            }

            var maxId = _borrowingList.Max(b => b.Id);
            borrowing.Id = maxId + 1;
            _borrowingList.Add(borrowing);

            BookRepository.MarkBookAsBorrowed(borrowing.BookId);
        }

        public static void UpdateBorrowing(int id, Borrowing borrowing)
        {
            if (id != borrowing.Id)
            {
                return;
            }
            var existingBorrowing = _borrowingList.FirstOrDefault(b => b.Id == borrowing.Id);
            if (existingBorrowing != null)
            {
                existingBorrowing.ReaderId = borrowing.ReaderId;
                existingBorrowing.BookId = borrowing.BookId;
                existingBorrowing.BorrowDate = borrowing.BorrowDate;
                existingBorrowing.ReturnDate = borrowing.ReturnDate;
            }
            else
            {
                throw new Exception("Borrowing not found");
            }
        }

        public static void DeleteBorrowing(int id)
        {
            var borrowing = _borrowingList.FirstOrDefault(b => b.Id == id);
            if (borrowing != null)
            {
                _borrowingList.Remove(borrowing);

                BookRepository.MarkBookAsReturned(borrowing.BookId);
            }
        }
    }
}