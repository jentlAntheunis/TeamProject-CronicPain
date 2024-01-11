using Pebbles.Models;
using Pebbles.Repositories;

namespace Pebbles.Services;

public interface IPatientService
{
    Task<Patient> GetPatientAsync(Guid id);
    Task<IEnumerable<Patient>> GetPatientsBySpecialistAsync(Guid SpecialistId);
    Task<Guid> AddPatientBySpecialistAsync(Guid SpecialistId, Patient patient);
    Task AddPatientToSpecialist(Guid PatientId, Guid SpecialistId);
    Task<Patient> UpdatePatientAsync(Patient patient);
    Task DeletePatientAsync(Patient patient);
}

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;
    private readonly ISpecialistRepository _specialistRepository;
    public PatientService(IPatientRepository patientRepository, ISpecialistRepository specialistRepository)
    {
        _patientRepository = patientRepository;
        _specialistRepository = specialistRepository;
    }

    public async Task<Patient> GetPatientAsync(Guid id) => await _patientRepository.GetPatientByIdAsync(id);

    public async Task<IEnumerable<Patient>> GetPatientsBySpecialistAsync(Guid SpecialistId)
    {
        var patients = await _patientRepository.GetAllPatientsAsync();
        var patientSpecialists = patients.SelectMany(p => p.PatientSpecialists).Where(ps => ps.SpecialistId == SpecialistId);
        return patientSpecialists.Select(ps => ps.Patient);
    }

    public async Task<Guid> AddPatientBySpecialistAsync(Guid SpecialistId, Patient patient)
    {
        var specialist = await _specialistRepository.GetSpecialistByIdAsync(SpecialistId);
        if (specialist == null)
            throw new Exception("Specialist does not exist");
        patient.PatientSpecialists.Add(new PatientSpecialist { PatientId = patient.Id, SpecialistId = SpecialistId });
        return await _patientRepository.CreatePatientAsync(patient);
    }

    public async Task AddPatientToSpecialist(Guid PatientId, Guid SpecialistId)
    {
        var patient = await _patientRepository.GetPatientByIdAsync(PatientId);
        var specialist = await _specialistRepository.GetSpecialistByIdAsync(SpecialistId);
        if (patient == null)
            throw new Exception("Patient does not exist");
        if(specialist == null)
            throw new Exception("Specialist does not exist");
        patient.PatientSpecialists.Add(new PatientSpecialist { PatientId = PatientId, SpecialistId = SpecialistId });
        await _patientRepository.UpdatePatientAsync(patient);
    }

    public async Task<Patient> UpdatePatientAsync(Patient patient) => await _patientRepository.UpdatePatientAsync(patient);
    public async Task DeletePatientAsync(Patient patient) => await _patientRepository.DeletePatientAsync(patient.Id);
}