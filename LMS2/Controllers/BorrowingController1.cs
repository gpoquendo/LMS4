using LMS2.Data;
using LMS2.Models;
using Microsoft.AspNetCore.Mvc;

namespace LMS2.Controllers
{
    [ApiController]
	[Route("[controller]")]
	public class BorrowingController : Controller
    {
        private readonly AppDbContext _context;

        public BorrowingController(AppDbContext context)
        {
            _context = context;
		}

        [HttpGet]
        public IActionResult Index()
        {
            var borrowings = _context.Borrowings.ToList();
            return Ok(borrowings);
        }

        [HttpGet("{id}")]
        public IActionResult GetBorrowing(int id)
        {
            var borrowing = _context.Borrowings.Find(id);
            if (borrowing == null)
            {
				return NotFound();
			}
            return Ok(borrowing);
        }

        [HttpPost]
        public IActionResult AddBorrowing([FromBody] Borrowing borrowing)
        {
            _context.Borrowings.Add(borrowing);
            _context.SaveChanges();
			return CreatedAtAction(nameof(GetBorrowing), new { id = borrowing.Id }, borrowing);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBorrowing(int id, [FromBody] Borrowing borrowing)
        {
			var existingBorrowing = _context.Borrowings.Find(id);
			if (existingBorrowing == null)
            {
                return NotFound();
            }
            existingBorrowing.BookId = borrowing.BookId;
            existingBorrowing.BorrowDate = borrowing.BorrowDate;
            existingBorrowing.ReaderId = borrowing.ReaderId;
            existingBorrowing.ReturnDate = borrowing.ReturnDate;
            _context.SaveChanges();

            return Ok(existingBorrowing);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBorrowing(int id)
        {
			var borrowing = _context.Borrowings.Find(id);
			if (borrowing == null)
            {
				return NotFound();
			}
			_context.Borrowings.Remove(borrowing);
            _context.SaveChanges();
			return Ok($"Reader has returned book with borrowing ID: {id}.");
		}
    }
}
