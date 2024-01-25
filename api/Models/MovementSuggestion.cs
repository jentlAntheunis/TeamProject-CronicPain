using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Pebbles.Models;

public class MovementSuggestion
{
    public Guid Id { get; set; }

    public Guid PatientId { get; set; }
    [JsonIgnore]
    public Patient Patient { get; set; }

    public Guid SpecialistId { get; set; }
    [JsonIgnore]
    public Specialist Specialist { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    public string VideoUrl { get; set; }

}