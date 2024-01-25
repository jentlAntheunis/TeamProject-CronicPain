using System.ComponentModel.DataAnnotations;

namespace Pebbles.Models;

public class Purchase
{

  [Key]
  public Guid PatientId { get; set; }
  public Patient Patient { get; set; }

  [Key]
  public Guid ColorId { get; set; }
  public Color Color { get; set; }
}
