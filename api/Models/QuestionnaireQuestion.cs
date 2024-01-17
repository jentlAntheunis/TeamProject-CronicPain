using System.ComponentModel.DataAnnotations.Schema;

namespace Pebbles.Models;

public class QuestionnaireQuestion
{
    public Guid QuestionnaireId { get; set; }
    [NotMapped]
    public Questionnaire Questionnaire { get; set; }
    public Guid QuestionId { get; set; }
    [NotMapped]
    public Question Question { get; set; }
}