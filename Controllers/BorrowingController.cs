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
            BorrowingRepository.AddBorrowing(borrowing);
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

            BorrowingRepository.UpdateBorrowing(id, borrowing);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Borrowing/DeleteBorrowing/{id}")]
        public IActionResult DeleteBorrowing(int id)
        {
            var borrowing = BorrowingRepository.GetBorrowingById(id);
            if (borrowing == null)
            {
                return NotFound();
            }

            BorrowingRepository.DeleteBorrowing(id);
            return RedirectToAction("Index");
        }
    }
}