using System.ComponentModel.DataAnnotations;

namespace Pebbles.Models;

public class Avatar : ISoftDelete
{
    public Avatar()
    {
        Id = Guid.NewGuid();
    }
    public Avatar(Guid patientId)
    {
        Id = Guid.NewGuid();
        PatientId = patientId;
    }
    public Guid Id { get; set; }

    [Required]
    public Guid PatientId { get; set; }
    public Patient Patient { get; set; }

    [Required]
    public Guid ColorId { get; set; }
    public Color Color { get; set; }

    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}