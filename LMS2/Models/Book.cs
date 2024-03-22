using System.ComponentModel.DataAnnotations;

namespace LMS2.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public bool IsAvailable { get; set; }
        public Borrowing? Borrowing { get; set; }
    }
}
