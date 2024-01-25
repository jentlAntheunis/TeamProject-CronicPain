using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Pebbles.Models;

public class QuestionnaireQuestion
{
    public Guid QuestionnaireId { get; set; }
    [NotMapped]
    [JsonIgnore]
    public Questionnaire Questionnaire { get; set; }
    public Guid QuestionId { get; set; }
    [NotMapped]
    [JsonIgnore]
    public Question Question { get; set; }
}