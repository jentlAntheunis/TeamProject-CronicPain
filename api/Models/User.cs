using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Pebbles.Models;

public class User : ISoftDelete
{
    public User(string firstName, string lastName, string email)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        Email = email;

        PatientSpecialists = new List<PatientSpecialist>();
        MovementSuggestions = new List<MovementSuggestion>();
    }

    [Key]
    public Guid Id { get; set; }

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

    [JsonIgnore]
    public List<PatientSpecialist> PatientSpecialists { get; set; }
    [JsonIgnore]
    public List<MovementSuggestion> MovementSuggestions { get; set; }
    public List<Login> Logins { get; set; }

    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }

    [NotMapped]
    public string Role { get; set; }
}