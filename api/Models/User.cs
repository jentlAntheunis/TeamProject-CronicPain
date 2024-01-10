using System.ComponentModel.DataAnnotations;

namespace Pebbles.Models;

public class User
{
    public Guid Id { get; set; }

    [Required]
    public Guid RoleId { get; set; }
    public Guid SpecialistId { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; }

    [Required]
    [StringLength(100)]
    public string FirstName { get; set; }

    [Required]
    [StringLength(100)]
    public string LastName { get; set; }

    public int Points { get; set; }
}