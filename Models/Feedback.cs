using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EventManagamentSystem.Models
{
    public class Feedback
    {
        [Key]
        public int FeedbackId { get; set; }

        [ForeignKey("Event")]
        public int EventId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [MaxLength(500)]
        public string Comment { get; set; } = null!;

        [Required]
        public DateTime FeedbackDate { get; set; } = DateTime.Now;

        public Event Event { get; set; }
        public User User { get; set; }
    }
}
