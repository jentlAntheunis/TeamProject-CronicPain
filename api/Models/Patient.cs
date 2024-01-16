using System.ComponentModel.DataAnnotations;

namespace Pebbles.Models;

public class Patient : User
{
    public Patient(string firstName, string lastName, string email) : base(firstName, lastName, email)
    {
        Specialists = new List<Specialist>();
        Avatar = new Avatar(Id);
        AvatarId = Avatar.Id;
        Colors = new List<Color>();
    }

    public int Streak { get; set; }
    public int Coins { get; set; }
    public Guid AvatarId { get; set; }
    public Avatar Avatar { get; set; }

    public List<Specialist> Specialists { get; set; }
    public List<Color> Colors { get; set; }
}