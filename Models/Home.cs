using System.ComponentModel.DataAnnotations;

namespace EventManagamentSystem.Models
{
    public class Home
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(500)]
        public string Description { get; set; } = null!;
    }
}
