namespace Pebbles.Models;

public class PatientSpecialist
{
    public Guid PatientId { get; set; }
    public Patient Patient { get; set; }
    public Guid SpecialistId { get; set; }
    public Specialist Specialist { get; set; }
}