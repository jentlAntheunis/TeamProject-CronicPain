using System.ComponentModel.DataAnnotations;
using System.Globalization;
namespace Pebbles.Models;
public class Specialist : User, ISoftDelete
{
    public Specialist() : base() { }
    public Specialist(string firstName, string lastName, string email) : base(firstName, lastName, email) { }

    public List<Patient> Patients { get; set; }

    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }
}