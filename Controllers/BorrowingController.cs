using LMS3.Models;
using LMS3.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LMS3.Controllers
{
    public class BorrowingController : Controller
    {
        [HttpGet]
        [Route("Borrowing")]
        public IActionResult Index()
        {
            var borrowings = BorrowingRepository.GetAllBorrowings();
            return View(borrowings);
        }

        [HttpGet]
        [Route("Borrowing/{id}")]
        public IActionResult GetBorrowing(int id)
        {
            var borrowing = BorrowingRepository.GetBorrowingById(id);
            if (borrowing == null)
            {
                return NotFound();
            }
            return View(borrowing);
        }

        [HttpGet]
        [Route("Borrowing/AddBorrowing")]
        public IActionResult AddBorrowing()
        {
            return View();
        }

        [HttpPost]
        [Route("Borrowing/AddBorrowing")]
        public IActionResult AddBorrowing(Borrowing borrowing)
        {
            try
            {
                BorrowingRepository.AddBorrowing(borrowing);

                var reader = ReaderRepository.GetReaderById(borrowing.ReaderId);
                if (reader != null)
                {
                    reader.BorrowedBooks.Add(borrowing);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(borrowing);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Borrowing/UpdateBorrowing/{id}")]
        public IActionResult UpdateBorrowing(int id)
        {
            var borrowing = BorrowingRepository.GetBorrowingById(id);
            if (borrowing == null)
            {
                return NotFound();
            }

            return View(borrowing);
        }

        [HttpPost]
        [Route("Borrowing/UpdateBorrowing/{id}")]
        public IActionResult UpdateBorrowing(int id, Borrowing borrowing)
        {
            var existingBorrowing = BorrowingRepository.GetBorrowingById(id);
            if (existingBorrowing == null)
            {
                return NotFound();
            }

            if (existingBorrowing.BookId != borrowing.BookId)
            {
                BookRepository.MarkBookAsReturned(existingBorrowing.BookId);
                BookRepository.MarkBookAsBorrowed(borrowing.BookId);
            }

            if (existingBorrowing.ReaderId != borrowing.ReaderId)
            {
                var oldReader = ReaderRepository.GetReaderById(existingBorrowing.ReaderId);
                if (oldReader != null)
                {
                    oldReader.BorrowedBooks.Remove(existingBorrowing);
                }

                var newReader = ReaderRepository.GetReaderById(borrowing.ReaderId);
                if (newReader != null)
                {
                    newReader.BorrowedBooks.Add(borrowing);
                }
            }

            BorrowingRepository.UpdateBorrowing(id, borrowing);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("Borrowing/DeleteBorrowing/{id}")]
        public IActionResult DeleteBorrowing(int id)
        {
            var borrowing = BorrowingRepository.GetBorrowingById(id);
            if (borrowing == null)
            {
                return NotFound();
            }

            BorrowingRepository.DeleteBorrowing(id);

            var reader = ReaderRepository.GetReaderById(borrowing.ReaderId);
            if (reader != null)
            {
                reader.BorrowedBooks.Remove(borrowing);
            }

            return RedirectToAction("Index");
        }
    }
}