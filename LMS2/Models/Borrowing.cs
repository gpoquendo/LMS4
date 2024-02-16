using System.ComponentModel.DataAnnotations;

namespace LMS2.Models
{
    public class Borrowing
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int BookId { get; set; }
        [Required]
        public DateTime BorrowDate { get; set; }
        [Required]
        public int ReaderId { get; set; }
        [Required]
        public DateTime ReturnDate { get; set; }
    }
}
