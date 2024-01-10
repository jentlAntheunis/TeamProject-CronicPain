using System.ComponentModel.DataAnnotations;

namespace Pebbles.Models;

public class Answer
{
    public Guid Id { get; set; }

    [Required]
    public Guid QuestionId { get; set; }

    [Required]
    public Guid OptionId { get; set; }

    [Required]
    public Guid QuestionnaireId { get; set; }
}