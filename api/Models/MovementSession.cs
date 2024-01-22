using Newtonsoft.Json;

namespace Pebbles.Models;

public class MovementSession
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    [JsonIgnore]
    public Patient Patient { get; set; }
    public int Seconds { get; set; }
}