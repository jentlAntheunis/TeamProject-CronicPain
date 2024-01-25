using Newtonsoft.Json;

namespace Pebbles.Models;

public class Login
{
  public Guid Id { get; set; }
  public Guid UserId { get; set; }
  [JsonIgnore]
  public User User { get; set; }
  public DateTime Timestamp { get; set; }
}
