using LMS2.Data;
using LMS2.Models;
using Microsoft.AspNetCore.Mvc;

namespace LMS2.Controllers
{
    [ApiController]
	[Route("[controller]")]
	public class ReaderController : Controller
    {
        private readonly AppDbContext _context;

        public ReaderController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var readers = _context.Readers.ToList();
            return Ok(readers);
        }

        [HttpGet("{id}")]
        public IActionResult GetReader(int id)
        {
			var reader = _context.Readers.Find(id);
			if (reader == null)
            {
				return NotFound();
			}
			return Ok(reader);
		}

        [HttpPost]
		public IActionResult AddReader([FromBody] Reader reader)
        {
			_context.Readers.Add(reader);
            _context.SaveChanges();
			return CreatedAtAction(nameof(GetReader), new { id = reader.ReaderId }, reader);
		}

        [HttpPut("{id}")]
		public IActionResult UpdateReader(int id, [FromBody] Reader reader)
        {
			var existingReader = _context.Readers.Find(id);
			if (existingReader == null)
            {
				return NotFound();
			}
			existingReader.Name = reader.Name;
            _context.SaveChanges();

			return Ok(existingReader);
		}

        [HttpDelete("{id}")]
        public IActionResult DeleteReader(int id)
        {
            var reader = _context.Readers.Find(id);
			if (reader == null)
            {
				return NotFound();
			}
			_context.Readers.Remove(reader);
            _context.SaveChanges();
			return Ok($"Reader with ID: {id} removed.");
        }
    }
}
