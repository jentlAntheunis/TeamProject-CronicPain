using System.ComponentModel.DataAnnotations;

namespace Pebbles.Models;

public class User : ISoftDelete
{
    public User()
    {
        Id = Guid.NewGuid();

        PatientSpecialists = new List<PatientSpecialist>();
        Questionnaires = new List<Questionnaire>();
        MovementSuggestions = new List<MovementSuggestion>();
    }
    public User(string firstName, string lastName, string email)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        Email = email;

        PatientSpecialists = new List<PatientSpecialist>();
        Questionnaires = new List<Questionnaire>();
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

    public List<PatientSpecialist> PatientSpecialists { get; set; }
    public List<Questionnaire> Questionnaires { get; set; }
    public List<MovementSuggestion> MovementSuggestions { get; set; }

    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}