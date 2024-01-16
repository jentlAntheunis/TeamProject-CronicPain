using Pebbles.Models;
using Pebbles.Repositories;
using System.Net.Mail;
using System.Net;
using System;

namespace Pebbles.Services;

public interface ISpecialistService
{
    Task<List<Specialist>> GetAllSpecialistsAsync();
    Task<Specialist> GetSpecialistByIdAsync(Guid id);
    Task<Specialist> CreateSpecialistAsync(Specialist specialist);
    Task<Specialist> UpdateSpecialistAsync(Specialist specialist);
    Task DeleteSpecialistAsync(Specialist specialist);
    void SendEmailWithInvitation(string specialistName, string specialistSurname, string patientName, string patientSurname, string patientEmail);
}

public class SpecialistService : ISpecialistService
{
    private readonly IConfiguration _configuration;
    private readonly ISpecialistRepository _specialistRepository;
    private readonly IPatientSpecialistRepository _patientSpecialistRepository;

    public SpecialistService(IConfiguration configuration)
    {
        _configuration = configuration;
        _specialistRepository = new SpecialistRepository(_configuration);
        _patientSpecialistRepository = new PatientSpecialistRepository(_configuration);
    }

    public async Task<List<Specialist>> GetAllSpecialistsAsync() => await _specialistRepository.GetAllSpecialistsAsync();

    public async Task<Specialist> GetSpecialistByIdAsync(Guid id) => await _specialistRepository.GetSpecialistByIdAsync(id);

    public async Task<Specialist> CreateSpecialistAsync(Specialist specialist) => await _specialistRepository.CreateSpecialistAsync(specialist);

    public async Task<Specialist> UpdateSpecialistAsync(Specialist specialist) => await _specialistRepository.UpdateSpecialistAsync(specialist);

    public async Task DeleteSpecialistAsync(Specialist specialist)
    {
        if (specialist != null)
        {
            //delete PatientSpecialist relations
            var patientSpecialists = specialist.PatientSpecialists;
            foreach (var patientSpecialist in patientSpecialists)
            {
                await _patientSpecialistRepository.DeletePatientSpecialistAsync(patientSpecialist);
            }
            await _specialistRepository.DeleteSpecialistAsync(specialist);
        }
    }
    public void SendEmailWithInvitation(string specialistName, string specialistSurname, string patientName, string patientSurname, string patientEmail)
    {
        try
        {
            Console.WriteLine("Versturen van e-mail...");

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp-auth.mailprotect.be");

            mail.From = new MailAddress(_configuration["EmailSettings:SmtpUsername"]);
            mail.To.Add(patientEmail);
            mail.Subject = "Uitnodiging voor Pebbles";
            mail.Body = "Beste " + patientName + " " + patientSurname + ",\n\n" +
                "Je bent uitgenodigd door " + specialistName + " " + specialistSurname + " om deel te nemen aan Pebbles.\n" +
                "Klik op de volgende link om je aan te melden: *link*\n\n" +
                "Met vriendelijke groeten,\n" +
                "Het Pebbles-team";

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new NetworkCredential(
                _configuration["EmailSettings:SmtpUsername"],
                _configuration["EmailSettings:SmtpPassword"]
            );
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