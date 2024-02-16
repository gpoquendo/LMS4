using System.ComponentModel.DataAnnotations;

namespace LMS2.Models
{
    public class Reader
    {
        [Key]
        public int ReaderId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
