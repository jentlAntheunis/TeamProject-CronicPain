using Pebbles.Models;
using Pebbles.Context;
using Microsoft.EntityFrameworkCore;

namespace Pebbles.Repositories;

public interface IPatientRepository
{
    Task<List<Patient>> GetAllPatientsAsync();
    Task<Patient> GetPatientByIdAsync(Guid id);
    Task<List<Patient>> GetPatientsBySpecialistIdAsync(Guid id);
    Task<Patient> GetPatientDetailsByIdAsync(Guid id);
    Task<Guid> CreatePatientAsync(Patient patient);
    Task<Patient> UpdatePatientAsync(Patient patient);
    Task DeletePatientAsync(Patient patient);
}

public class PatientRepository : IPatientRepository
{
    private readonly PebblesContext _context;

    public PatientRepository(PebblesContext context)
    {
        _context = context;
    }

    public async Task<List<Patient>> GetAllPatientsAsync()
    {
        return await _context.Patient
            .Include(p => p.Avatar)
            .Where(p => p.IsDeleted == false)
            .ToListAsync();
    }

    public async Task<Patient> GetPatientByIdAsync(Guid id)
    {
        return await _context.Patient
            .Include(p => p.Avatar)
            .ThenInclude(a => a.Color)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Patient> GetPatientDetailsByIdAsync(Guid id)
    {
        return await _context.Patient
            .Include(p => p.Avatar).ThenInclude(a => a.Color)
            .Include(p => p.Colors)
            .Include(p => p.MovementSessions)
            .Include(p => p.MovementSuggestions)
            .Include(p => p.Logins)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Patient>> GetPatientsBySpecialistIdAsync(Guid id)
    {
        return await _context.Patient
            .Include(p => p.Avatar)
            .Include(p => p.Specialists)
            .Where(p => p.Specialists.Any(s => s.Id == id))
            .ToListAsync();
    }

    public async Task<Guid> CreatePatientAsync(Patient patient)
    {
        await _context.Patient.AddAsync(patient);
        await _context.SaveChangesAsync();
        return patient.Id;
    }

    public async Task<Patient> UpdatePatientAsync(Patient patient)
    {
        _context.Patient.Update(patient);
        await _context.SaveChangesAsync();
        return patient;
    }

    public async Task DeletePatientAsync(Patient patient)
    {
        //check if patient has an avatar
        var avatar = await _context.Avatar.FirstOrDefaultAsync(a => a.Id == patient.AvatarId);
        if (avatar != null)
        {
            //delete avatar
            _context.Avatar.Remove(avatar);
        }
        //delete patient
        _context.Patient.Remove(patient);
        await _context.SaveChangesAsync();
    }
}