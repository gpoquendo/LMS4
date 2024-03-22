using LMS2.Data;
using LMS2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMS2.Controllers
{
	public class ReaderController : Controller
    {
        private readonly AppDbContext _context;

        public ReaderController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("Reader")]
        public IActionResult Index()
        {
			var readers = _context.Readers
				.Include(r => r.Borrowings)
				.ThenInclude(b => b.Book)
				.ToList();
			return View("Readers", readers);
		}

        [HttpGet("Reader/{id}")]
        public IActionResult GetReader(int id)
        {
            var readerWithBorrowings = _context.Readers
		        .Include(r => r.Borrowings)
		        .ThenInclude(b => b.Book)
		        .FirstOrDefault(r => r.ReaderId == id);

            if (readerWithBorrowings == null)
            {
                return NotFound();
            }
            return Ok(readerWithBorrowings);
        }

        [HttpGet("/Reader/Add")]
        public IActionResult AddReader()
        {
            return View();
        }

        [HttpPost("/Reader/Add")]
		public IActionResult AddReader([Bind("Name")] Reader reader)
        {
            _context.Readers.Add(reader);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

		[HttpGet("/Reader/Update/{id}")]
		public IActionResult UpdateReader(int id)
		{
			var reader = _context.Readers.Find(id);
			if (reader == null)
			{
				return NotFound();
			}
			return View(reader);
		}

		[HttpPost("/Reader/Update/{id}")]
		public IActionResult UpdateReader(int id, [Bind("Name")] Reader reader)
        {
			var existingReader = _context.Readers.Find(id);
			if (existingReader == null)
			{
				return NotFound();
			}
			existingReader.Name = reader.Name;
			_context.SaveChanges();

			return RedirectToAction(nameof(Index));
		}

        [HttpPost("Reader/Delete/{id}")]
        public IActionResult DeleteReader(int id)
        {
			var reader = _context.Readers.Find(id);
			if (reader == null)
			{
				return NotFound();
			}
			_context.Readers.Remove(reader);
			_context.SaveChanges();
			return RedirectToAction(nameof(Index));
		}
    }
}
