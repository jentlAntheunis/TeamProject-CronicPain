using System.ComponentModel.DataAnnotations;

namespace Pebbles.Models;

public class MovementSuggestion
{
    public Guid Id { get; set; }

    public Guid PatientId { get; set; }
    public Patient Patient { get; set; }

    public Guid SpecialistId { get; set; }
    public Specialist Specialist { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    public string VideoUrl { get; set; }

}