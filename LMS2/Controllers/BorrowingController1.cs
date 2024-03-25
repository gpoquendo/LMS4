using LMS2.Data;
using LMS2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS2.Controllers
{
	public class BorrowingController : Controller
	{
		private readonly AppDbContext _context;

		public BorrowingController(AppDbContext context)
		{
			_context = context;
		}

		[HttpGet("Borrowing")]
		public IActionResult Index()
		{
			var borrowings = _context.Borrowings
				.Include(b => b.Book)
				.Include(b => b.Reader)
				.ToList();
			return View("Borrowings", borrowings);
		}

		[HttpGet("Borrowing/{id}")]
		public IActionResult GetBorrowing(int id)
		{
			var borrowing = _context.Borrowings
				.Include(b => b.Book)
				.Include(b => b.Reader)
				.FirstOrDefault(b => b.Id == id);
			if (borrowing == null)
			{
				return NotFound();
			}
			return View("Borrowings", borrowing);
		}

		[HttpGet("Borrowing/Add")]
		public IActionResult AddBorrowing()
		{
			ViewBag.Readers = _context.Readers.ToList();
			ViewBag.Books = _context.Books.Where(b => b.IsAvailable).ToList();
			return View();
		}

		[HttpPost("Borrowing/Add")]
		public async Task<IActionResult> AddBorrowing(int bookId, int readerId)
		{
			var book = await _context.Books.FindAsync(bookId);
			if (book == null)
			{
				return NotFound("Book not found");
			}

			if (!book.IsAvailable)
			{
				return BadRequest("Book is not available");
			}

			var reader = await _context.Readers.FindAsync(readerId);
			if (reader == null)
			{
				return NotFound("Reader not found");
			}

			var borrowing = new Borrowing
			{
				BookId = bookId,
				ReaderId = readerId,
				BorrowDate = DateTime.Now,
				ReturnDate = DateTime.Now.AddDays(14),
			};

			book.IsAvailable = false;
			book.Borrowing = borrowing;
			_context.Borrowings.Add(borrowing);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		[HttpGet("Borrowing/Update/{id}")]
		public IActionResult UpdateBorrowing(int id)
		{
			var borrowing = _context.Borrowings
				.Include(b => b.Reader)
				.Include(b => b.Book)
				.SingleOrDefault(b => b.Id == id);

			if (borrowing == null)
			{
				return NotFound();
			}
			return View(borrowing);
		}

		[HttpPost("Borrowing/Update/{id}")]
		public async Task<IActionResult> UpdateBorrowing(int id, Borrowing borrowing)
		{
			var existingBorrowing = await _context.Borrowings.FindAsync(id);
			if (existingBorrowing == null)
			{
				return NotFound();
			}

			if (borrowing.ReturnDate < existingBorrowing.BorrowDate)
			{
				return BadRequest("Return date cannot be before borrow date");
			}

			existingBorrowing.BorrowDate = borrowing.BorrowDate;
			existingBorrowing.ReturnDate = borrowing.ReturnDate;

			await _context.SaveChangesAsync();

			return RedirectToAction(nameof(Index));
		}

		[HttpPost("Borrowing/Delete/{id}")]
		public async Task<IActionResult> DeleteBorrowing(int id)
		{
			var borrowing = await _context.Borrowings.FindAsync(id);
			if (borrowing == null)
			{
				return NotFound("Borrowing transaction not found");
			}

			var book = await _context.Books.FindAsync(borrowing.BookId);
			if (book == null)
			{
				return NotFound("Book not found");
			}

			book.IsAvailable = true;
			_context.Borrowings.Remove(borrowing);
			await _context.SaveChangesAsync();

			return RedirectToAction(nameof(Index));
		}
	}
}
