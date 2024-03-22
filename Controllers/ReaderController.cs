using LMS3.Models;
using LMS3.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LMS3.Controllers
{
    public class ReaderController : Controller
    {
        [HttpGet]
        [Route("Reader")]
        public IActionResult Index()
        {
            var readers = ReaderRepository.GetAllReaders();
            foreach (var reader in readers)
            {
                reader.BorrowedBooks = ReaderRepository.GetBorrowedBooksByReaderId(reader.ReaderId);
            }

            return View(readers);
        }

        [HttpGet]
        [Route("Reader/{id}")]
        public IActionResult GetReader(int id)
        {
            var reader = ReaderRepository.GetReaderById(id);
            if (reader == null)
            {
                return NotFound();
            }
            reader.BorrowedBooks = ReaderRepository.GetBorrowedBooksByReaderId(id);

            return View(reader);
        }

        [HttpGet]
        [Route("Reader/AddReader")]
        public IActionResult AddReader()
        {
            return View();
        }

        [HttpPost]
        [Route("Reader/AddReader")]
        public IActionResult AddReader(Reader reader)
        {
            ReaderRepository.AddReader(reader);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Reader/UpdateReader/{id}")]
        public IActionResult UpdateReader(int id)
        {
            var reader = ReaderRepository.GetReaderById(id);
            if (reader == null)
            {
                return NotFound();
            }

            return View(reader);
        }

        [HttpPost]
        [Route("Reader/UpdateReader/{id}")]
        public IActionResult UpdateReader(int id, Reader reader)
        {
            var existingReader = ReaderRepository.GetReaderById(id);
            if (existingReader == null)
            {
                return NotFound();
            }
            ReaderRepository.UpdateReader(reader);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("Reader/DeleteReader/{id}")]
        public IActionResult DeleteReader(int id)
        {
            var reader = ReaderRepository.GetReaderById(id);
            if (reader == null)
            {
                return NotFound();
            }

            ReaderRepository.DeleteReader(id);
            return RedirectToAction("Index");
        }
    }
}