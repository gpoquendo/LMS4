using LMS3.Models;
using Microsoft.AspNetCore.Mvc;

namespace LMS2.Controllers
{
    public class BookController : Controller
    {
        [HttpGet]
        [Route("Book")]
        public IActionResult Index()
        {
            var books = BookRepository.GetAllBooks();
            return View(books);
        }

        [HttpGet]
        [Route("Book/{id}")]
        public IActionResult GetBook(int id)
        {
            var book = BookRepository.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        [HttpGet]
        [Route("Book/AddBook")]
        public IActionResult AddBook()
        {
            return View();
        }

        [HttpPost]
        [Route("Book/AddBook")]
        public IActionResult AddBook(Book book)
        {
            BookRepository.AddBook(book);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Book/UpdateBook/{id}")]
        public IActionResult UpdateBook(int id)
        {
            var book = BookRepository.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost]
        [Route("Book/UpdateBook/{id}")]
        public IActionResult UpdateBook(int id, Book book)
        {
            var existingBook = BookRepository.GetBookById(id);
            if (existingBook == null)
            {
                return NotFound();
            }

            BookRepository.UpdateBook(book);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("Book/DeleteBook/{id}")]
        public IActionResult DeleteBook(int id)
        {
            BookRepository.DeleteBook(id);
            return RedirectToAction("Index");
        }
    }
}
