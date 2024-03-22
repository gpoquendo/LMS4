using LMS2.Data;
using LMS2.Models;
using Microsoft.AspNetCore.Mvc;

namespace LMS2.Controllers
{
    public class BookController : Controller
    {

        private readonly AppDbContext _context;

        public BookController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("/Book")]
        public IActionResult GetAllBooks()
        {
            var books = _context.Books.ToList();
            return View("Books", books);
        }

        [HttpGet("/Book/{id}")]
        public IActionResult GetBook(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpGet("/Book/Add")]
        public IActionResult AddBook()
        {
            return View();
        }

        [HttpPost("/Book/Add")]
        public IActionResult AddBook([Bind("Title,Author")] Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
            return RedirectToAction(nameof(GetAllBooks));
        }

		[HttpGet("/Book/Update/{id}")]
		public IActionResult UpdateBook(int id)
		{
			var book = _context.Books.Find(id);
			if (book == null)
			{
				return NotFound();
			}
			return View(book);
		}

		[HttpPost("/Book/Update/{id}")]
        public IActionResult UpdateBook(int id, [Bind("Title,Author")] Book book)
        {
            var existingBook = _context.Books.Find(id);
            if (existingBook == null)
            {
                return NotFound();
            }
            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            _context.SaveChanges();

            return RedirectToAction(nameof(GetAllBooks));
        }

        [HttpPost("/Book/Delete/{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }
            _context.Books.Remove(book);
            _context.SaveChanges();
			return RedirectToAction(nameof(GetAllBooks));
		}
    }
}
