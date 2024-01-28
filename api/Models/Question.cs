using System.ComponentModel.DataAnnotations;

namespace Pebbles.Models;

public class Question :ISoftDelete
{
    public Guid Id { get; set; }

    [Required]
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }

    public List<Questionnaire> Questionnaires { get; set; }

    public Guid? SpecialistId { get; set; }

    public Guid ScaleId { get; set; }
    public Scale Scale { get; set; }

    public string Content { get; set; }

    public List<Answer> Answers { get; set; }

    public bool IsDeleted { get; set; }

    public DateTimeOffset? DeletedAt { get; set; }
}
