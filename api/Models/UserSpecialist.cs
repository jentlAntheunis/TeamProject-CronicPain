using System.ComponentModel.DataAnnotations;

namespace Pebbles.Models;

public class UserSpecialist
{
    [Required]
    public Guid UserId { get; set; }
    public User User { get; set; }

    [Required]
    public Guid SpecialistId { get; set; }
    public User Specialist { get; set; }
}