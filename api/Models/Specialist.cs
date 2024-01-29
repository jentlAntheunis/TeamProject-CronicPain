using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Newtonsoft.Json;

namespace Pebbles.Models;
public class Specialist : User
{
    public Specialist(string firstName, string lastName, string email) : base(firstName, lastName, email)
    {
        Role = "SPECIALIST";
    }

    [JsonIgnore]
    public List<Patient> Patients { get; set; }
}