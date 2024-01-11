namespace Pebbles.Models;

public class Patient : User
{
    public Patient()
    {
        PatientSpecialists = new List<PatientSpecialist>();
        Avatars = new List<Avatar>();
    }

    public Patient(string firstName, string lastName, string email)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        Email = email;

        PatientSpecialists = new List<PatientSpecialist>();
        Avatars = new List<Avatar>();
    }

    public List<Specialist> Specialists { get; set; }
    public List<PatientSpecialist> PatientSpecialists { get; set; }
    public List<Avatar> Avatars { get; set; }

}