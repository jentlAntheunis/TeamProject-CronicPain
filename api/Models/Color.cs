using System.ComponentModel.DataAnnotations;

namespace Pebbles.Models;

public class Color : ISoftDelete
{
    public Color(string name, string hex)
    {
        Id = Guid.NewGuid();
        Name = name;
        Hex = hex;
        Price = 0;

        Patients = new List<Patient>();
        Avatars = new List<Avatar>();
    }

    public Color(string name, string hex, int price)
    {
        Id = Guid.NewGuid();
        Name = name;
        Hex = hex;
        Price = price;

        Patients = new List<Patient>();
        Avatars = new List<Avatar>();
    }
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Hex { get; set; }

    [Required]
    public int Price { get; set; }

    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }

    public List<Patient> Patients { get; set; }
    public List<Avatar> Avatars { get; set; }
}