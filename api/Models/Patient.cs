namespace Pebbles.Models;

public class Patient : User
{
    public Patient() { }

    public Patient(string firstName, string lastName, string email)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    public List<Specialist> Specialists { get; set; }
    public List<PatientSpecialist> PatientSpecialists { get; set; }
    public List<Avatar> Avatars { get; set; }
    
}