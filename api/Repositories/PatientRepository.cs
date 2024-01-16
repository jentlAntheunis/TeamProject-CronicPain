using Pebbles.Models;
using Pebbles.Context;
using Microsoft.EntityFrameworkCore;

namespace Pebbles.Repositories;

public interface IPatientRepository
{
    Task<List<Patient>> GetAllPatientsAsync();
    Task<Patient> GetPatientByIdAsync(Guid id);
    Task<Guid> CreatePatientAsync(Patient patient);
    Task<Patient> UpdatePatientAsync(Patient patient);
    Task DeletePatientAsync(Patient patient);
}

public class PatientRepository : IPatientRepository
{
    private readonly PebblesContext _context;

    public PatientRepository(IConfiguration configuration)
    {
        _context = new PebblesContext(configuration);
    }

    public async Task<List<Patient>> GetAllPatientsAsync() => await _context.Patient.Where(p => p.IsDeleted == false).ToListAsync();

    public async Task<Patient> GetPatientByIdAsync(Guid id) => await _context.Patient.Where(p => p.IsDeleted == false).FirstOrDefaultAsync(p => p.Id == id);

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
        _context.Patient.Remove(patient);
        await _context.SaveChangesAsync();
    }
}