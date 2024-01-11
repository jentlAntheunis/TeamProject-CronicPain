using Pebbles.Models;
using Pebbles.Repositories;

namespace Pebbles.Services;

public interface ISpecialistRepository
{
    Task<List<Specialist>> GetAllSpecialistsAsync();
    Task<Specialist> GetSpecialistByIdAsync(Guid id);
    Task<Specialist> CreateSpecialistAsync(Specialist specialist);
    Task<Specialist> UpdateSpecialistAsync(Specialist specialist);
    Task DeleteSpecialistAsync(Guid id);
}

public class SpecialistService : ISpecialistService
{
    private readonly ISpecialistRepository _specialistRepository;

    public SpecialistService(ISpecialistRepository specialistRepository)
    {
        _specialistRepository = specialistRepository;
    }

    public async Task<List<Specialist>> GetAllSpecialistsAsync() => await _specialistRepository.GetAllSpecialistsAsync();

    public async Task<Specialist> GetSpecialistByIdAsync(Guid id) => await _specialistRepository.GetSpecialistByIdAsync(id);

    public async Task<Specialist> CreateSpecialistAsync(Specialist specialist) => await _specialistRepository.CreateSpecialistAsync(specialist);

    public async Task<Specialist> UpdateSpecialistAsync(Specialist specialist) => await _specialistRepository.UpdateSpecialistAsync(specialist);

    public async Task DeleteSpecialistAsync(Guid id) => await _specialistRepository.DeleteSpecialistAsync(id);

    public void SendEmail(string afzenderVoornaam, string afzenderAchternaam, string ontvangerVoornaam, string ontvangerAchternaam, string ontvangerEmail)
    {
        try
        {
            Console.WriteLine("Versturen van e-mail...");

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp-auth.mailprotect.be");

            mail.From = new MailAddress("noreply@pebbles-health.be");
            mail.To.Add(ontvangerEmail); 
            mail.Subject = "Uitnodiging voor Pebbles";
            mail.Body = "Beste " + ontvangerVoornaam + " " + ontvangerAchternaam + ",\n\n" +
                "Je bent uitgenodigd door " + afzenderVoornaam + " " + afzenderAchternaam + " om deel te nemen aan Pebbles.\n" +
                "Klik op de volgende link om je aan te melden: *link*\n\n" +
                "Met vriendelijke groeten,\n" +
                "Het Pebbles-team";

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new NetworkCredential("noreply@pebbles-health.be", "88H2i5o615L4Ei8vxxY8");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);

            Console.WriteLine("Mail Verstuurd");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Fout bij het versturen van e-mail: " + ex.Message);
        }
    }


}