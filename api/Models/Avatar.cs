using System.ComponentModel.DataAnnotations;

namespace Pebbles.Models;

public class Avatar
{
    public Guid Id { get; set; }

    [Required]
    public Guid PatientId { get; set; }
    public Patient Patient { get; set; }

    [Required]
    public Guid ColorId { get; set; }
    public Color Color { get; set; }
}