using Newtonsoft.Json;

namespace Pebbles.Models;

public class Login
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    [JsonIgnore]
    public Patient Patient { get; set; }
    public DateTime Timestamp { get; set; }
}