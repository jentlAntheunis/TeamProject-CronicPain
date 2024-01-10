using System.ComponentModel.DataAnnotations;

namespace Pebbles.Models;

public class Color
{
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Hex { get; set; }

    [Required]
    public int Price { get; set; }
}