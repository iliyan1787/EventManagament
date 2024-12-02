using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EventManagamentSystem.Models
{
    public class Attendee
    {
        [Key]
        public int AttendeeId { get; set; }

        [ForeignKey("Event")]
        public int EventId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        [Required]
        public DateTime RSVPDate { get; set; }

        public Event Event { get; set; }
        public User User { get; set; }
    }
}
