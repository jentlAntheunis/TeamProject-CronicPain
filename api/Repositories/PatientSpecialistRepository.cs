using Pebbles.Context;
using Pebbles.Models;
using Microsoft.EntityFrameworkCore;

namespace Pebbles.Repositories;

public interface IPatientSpecialistRepository
{
    Task<List<PatientSpecialist>> GetPatientSpecialistsAsync();
    Task<List<PatientSpecialist>> GetPatientSpecialistsByIdAsync(Guid Id);
    Task<List<PatientSpecialist>> GetPatientSpecialistsByPatientIdAsync(Guid Id);
    Task<List<PatientSpecialist>> GetPatientSpecialistsBySpecialistIdAsync(Guid Id);
    Task DeletePatientSpecialistAsync(PatientSpecialist patientSpecialist);
}

public class PatientSpecialistRepository : IPatientSpecialistRepository
{
    private readonly PebblesContext _context;

    public PatientSpecialistRepository(IConfiguration configuration)
    {
        _context = new PebblesContext(configuration);
    }

    public async Task<List<PatientSpecialist>> GetPatientSpecialistsAsync() => await _context.PatientSpecialist.ToListAsync();

    public async Task<List<PatientSpecialist>> GetPatientSpecialistsByIdAsync(Guid Id) => await _context.PatientSpecialist.Where(ps => ps.PatientId == Id || ps.SpecialistId == Id).ToListAsync();

    public async Task<List<PatientSpecialist>> GetPatientSpecialistsByPatientIdAsync(Guid Id) => await _context.PatientSpecialist.Where(ps => ps.PatientId == Id).ToListAsync();

    public async Task<List<PatientSpecialist>> GetPatientSpecialistsBySpecialistIdAsync(Guid Id) => await _context.PatientSpecialist.Where(ps => ps.SpecialistId == Id).ToListAsync();

    public async Task DeletePatientSpecialistAsync(PatientSpecialist patientSpecialist)
    {
        _context.PatientSpecialist.Remove(patientSpecialist);
        await _context.SaveChangesAsync();
    }
}