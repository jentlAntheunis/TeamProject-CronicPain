using System.ComponentModel.DataAnnotations;

namespace Pebbles.Models;

public class Option
{
    public Guid Id { get; set; }

    [Required]
    public Guid ScaleId { get; set; }

    public string Content { get; set; }
    public string Position { get; set; }
}