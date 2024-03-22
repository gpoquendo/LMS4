using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LMS3.Models
{
    public class Borrowing
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Book ID")]
        public int BookId { get; set; }
        [Required]
        [DisplayName("Borrow Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BorrowDate { get; set; }
        [Required]
        [DisplayName("Reader ID")]
        public int ReaderId { get; set; }
        [Required]
        [DisplayName("Return Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReturnDate { get; set; }
    }
}
