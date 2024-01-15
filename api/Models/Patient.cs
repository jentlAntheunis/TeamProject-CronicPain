using System.ComponentModel.DataAnnotations;

namespace Pebbles.Models;

public class Patient : User
{
    public Patient() : base() { }
    public Patient(string firstName, string lastName, string email) : base(firstName, lastName, email) { }

    public Guid AvatarId { get; set; }
    public Avatar Avatar { get; set; }

    public List<Specialist> Specialists { get; set; }
    public List<Color> Colors { get; set; }

}