using System.ComponentModel.DataAnnotations;

namespace Pebbles.Models;

public class Scale
{
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    public List<Option> Options { get; set; }
    public List<Question> Questions { get; set; }
}