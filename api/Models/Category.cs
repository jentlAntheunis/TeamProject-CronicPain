using System.ComponentModel.DataAnnotations;

namespace Pebbles.Models;

public class Category
{
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }
}