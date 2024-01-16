namespace Pebbles.Models;

public class PatientSpecialist : ISoftDelete
{
    public Guid PatientId { get; set; }
    public Patient Patient { get; set; }
    public Guid SpecialistId { get; set; }
    public Specialist Specialist { get; set; }

    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}