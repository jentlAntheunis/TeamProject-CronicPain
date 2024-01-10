using System.ComponentModel.DataAnnotations;

namespace Pebbles.Models;

public class Questionnaire
{
    public Guid Id { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required]
    public DateTime Date { get; set; }

    public List<Answer> Answers { get; set; }
}