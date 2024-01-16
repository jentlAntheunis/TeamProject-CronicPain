using Pebbles.Models;
using Pebbles.Context;

namespace Pebbles.Repositories;

public interface IPatientSpecialistRepository
{
    Task DeletePatientSpecialistAsync(PatientSpecialist patientSpecialist);
}

public class PatientSpecialistRepository : IPatientSpecialistRepository
{
    private readonly PebblesContext _context;

    public PatientSpecialistRepository(PebblesContext context)
    {
        _context = context;
    }

    public async Task DeletePatientSpecialistAsync(PatientSpecialist patientSpecialist)
    {
        _context.PatientSpecialist.Remove(patientSpecialist);
        await _context.SaveChangesAsync();
    }
}