using System.ComponentModel.DataAnnotations;

namespace Pebbles.Models;

public class Answer
{
    public Guid Id { get; set; }

    [Required]
    public Guid QuestionId { get; set; }
    public Question Question { get; set; }

    [Required]
    public Guid OptionId { get; set; }
    public Option Option { get; set; }

    [Required]
    public Guid QuestionnaireId { get; set; }
    public Questionnaire Questionnaire { get; set; }
}