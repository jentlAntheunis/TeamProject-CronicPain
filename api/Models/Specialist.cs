
namespace Pebbles.Models;
public class Specialist : User
{
    public Specialist() { }
    public Specialist(string firstName, string lastName, string email)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    public List<Patient> Patients { get; set; }
    public List<PatientSpecialist> PatientSpecialists { get; set; }
}