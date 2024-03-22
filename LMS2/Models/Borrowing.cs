using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS2.Models
{
    public class Borrowing
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Book")]
        public int BookId { get; set; }
        [Required]
        [ForeignKey("Reader")]
        public int ReaderId { get; set; }
        [Required]
        public DateTime BorrowDate { get; set; }
        [Required]
        public DateTime ReturnDate { get; set; }

        public Book Book { get; set; }
        public Reader Reader { get; set; }
    }
}
