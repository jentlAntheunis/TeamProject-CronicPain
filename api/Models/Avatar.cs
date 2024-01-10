using System.ComponentModel.DataAnnotations;

namespace Pebbles.Models;

public class Avatar
{
    public Guid Id { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required]
    public Guid ColorId { get; set; }
}