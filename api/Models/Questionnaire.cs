using System.ComponentModel.DataAnnotations;

namespace Pebbles.Models;

public class Questionnaire
{
    public Guid Id { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required]
    public DateTime Date { get; set; }

    public Guid PatientId { get; set; }
    public Patient Patient { get; set; }
    public Guid SpecialistId { get; set; }
    public Specialist Specialist { get; set; }
    public List<Question> Questions { get; set; }
}