using EventManagamentSystem.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class User
{
    [Key]
    public int UserId { get; set; }

    [Required]
    [MaxLength(50)]
    public string Username { get; set; } = null!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [Required]
    public string PasswordHash { get; set; } = null!;

    [Required]
    public string Role { get; set; } = null!; // User or Admin 

    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public ICollection<Attendee> Attendees { get; set; } = new List<Attendee>();

}