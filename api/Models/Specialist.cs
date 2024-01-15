using System.ComponentModel.DataAnnotations;
using System.Globalization;
namespace Pebbles.Models;
public class Specialist : User
{
    public Specialist() : base() { }
    public Specialist(string firstName, string lastName, string email) : base(firstName, lastName, email) { }

    public List<Patient> Patients { get; set; }
}