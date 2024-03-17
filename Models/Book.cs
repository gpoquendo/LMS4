using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LMS3.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [DisplayName("Availability")]
        public bool IsAvailable { get; set; } = true;
    }
}
