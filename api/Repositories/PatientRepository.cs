using Pebbles.Models;
using Pebbles.Context;
using Microsoft.EntityFrameworkCore;

namespace Pebbles.Repositories;

public interface IPatientRepository
{
    Task<List<Patient>> GetAllPatientsAsync();
    Task<Patient> GetPatientByIdAsync(Guid id);
    Task<Patient> CreatePatientAsync(Patient patient);
    Task<Patient> UpdatePatientAsync(Patient patient);
    Task DeletePatientAsync(Guid id);
}

public class PatientRepository : IPatientRepository
{
    private readonly PebblesContext _context;

    public PatientRepository(PebblesContext context)
    {
        _context = context;
    }

    public async Task<List<Patient>> GetAllPatientsAsync() => await _context.Patient.ToListAsync();

    public async Task<Patient> GetPatientByIdAsync(Guid id) => await _context.Patient.FirstOrDefaultAsync(p => p.Id == id);

    public async Task<Patient> CreatePatientAsync(Patient patient)
    {
        await _context.Patient.AddAsync(patient);
        await _context.SaveChangesAsync();
        return patient;
    }

    public async Task<Patient> UpdatePatientAsync(Patient patient)
    {
        _context.Patient.Update(patient);
        await _context.SaveChangesAsync();
        return patient;
    }

    public async Task DeletePatientAsync(Guid id)
    {
        var patient = await GetPatientByIdAsync(id);
        _context.Patient.Remove(patient);
        await _context.SaveChangesAsync();
    }
}