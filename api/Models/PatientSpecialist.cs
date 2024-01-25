using Newtonsoft.Json;

namespace Pebbles.Models;

public class PatientSpecialist
{
  public Guid PatientId { get; set; }
  [JsonIgnore]
  public Patient Patient { get; set; }
  public Guid SpecialistId { get; set; }
  [JsonIgnore]
  public Specialist Specialist { get; set; }
}
