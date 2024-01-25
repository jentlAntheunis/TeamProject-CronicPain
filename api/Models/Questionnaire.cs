using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Pebbles.Models;

public class Questionnaire
{
    public Guid Id { get; set; }
    public DateTime ? Date { get; set; }

    [Required]
    public Guid PatientId { get; set; }
    [JsonIgnore]
    public Patient Patient { get; set; }
    public List<Question> Questions { get; set; }
}