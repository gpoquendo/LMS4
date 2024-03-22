using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LMS3.Models
{
    public class Reader
    {
        [Key]
        public int ReaderId { get; set; }
        [Required]
        public string Name { get; set; }
        [DisplayName("Books Borrowed")]
        public List<Borrowing>? BorrowedBooks { get; set; }
    }
}
