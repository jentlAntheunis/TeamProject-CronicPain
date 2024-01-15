using System.ComponentModel.DataAnnotations;

namespace Pebbles.Models;

public class User
{
    public User()
    {
        Id = Guid.NewGuid();
    }
    public User(string firstName, string lastName, string email)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        Email = email;
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

    public List<PatientSpecialist> PatientSpecialists { get; set; }
    public List<Questionnaire> Questionnaires { get; set; }
    public List<MovementSuggestion> MovementSuggestions { get; set; }
}