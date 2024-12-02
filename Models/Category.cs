using System.ComponentModel.DataAnnotations;

namespace EventManagamentSystem.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;
    }
}
