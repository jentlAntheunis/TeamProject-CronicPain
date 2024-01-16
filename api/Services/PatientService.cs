using Pebbles.Models;
using Pebbles.Repositories;

namespace Pebbles.Services;

public interface IPatientService
{
    Task<Patient> GetPatientByIdAsync(Guid id);
    Task<IEnumerable<Patient>> GetPatientsBySpecialistAsync(Guid SpecialistId);
    Task<Guid> AddPatientBySpecialistAsync(Guid SpecialistId, Patient patient);
    Task AddPatientToSpecialistAsync(Guid PatientId, Guid SpecialistId);
    Task<Patient> UpdatePatientAsync(Patient patient);
    Task DeletePatientAsync(Patient patient);
}

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;
    private readonly ISpecialistRepository _specialistRepository;
    private readonly IColorRepository _colorRepository;
    private readonly IAvatarRepository _avatarRepository;
    private readonly IPatientSpecialistRepository _patientSpecialistRepository;
    public PatientService(IConfiguration configuration)
    {
        _patientRepository = new PatientRepository(configuration);
        _specialistRepository = new SpecialistRepository(configuration);
        _colorRepository = new ColorRepository(configuration);
        _avatarRepository = new AvatarRepository(configuration);
        _patientSpecialistRepository = new PatientSpecialistRepository(configuration);
    }

    public async Task<Patient> GetPatientByIdAsync(Guid id) => await _patientRepository.GetPatientByIdAsync(id);

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

        var color = await _colorRepository.GetDefaultColorAsync();
        patient.Avatar.ColorId = color.Id;
        return await _patientRepository.CreatePatientAsync(patient);
    }

    public async Task AddPatientToSpecialistAsync(Guid PatientId, Guid SpecialistId)
    {
        var patient = await _patientRepository.GetPatientByIdAsync(PatientId);
        var specialist = await _specialistRepository.GetSpecialistByIdAsync(SpecialistId);
        if (patient == null)
            throw new Exception("Patient does not exist");
        if (specialist == null)
            throw new Exception("Specialist does not exist");
        patient.PatientSpecialists.Add(new PatientSpecialist { PatientId = PatientId, SpecialistId = SpecialistId });
        await _patientRepository.UpdatePatientAsync(patient);
    }

    public async Task<Patient> UpdatePatientAsync(Patient patient) => await _patientRepository.UpdatePatientAsync(patient);

    public async Task DeletePatientAsync(Patient patient)
    {
        //check if patient has an avatar
        var avatar = await _avatarRepository.GetAvatarByIdAsync(patient.AvatarId);
        if (avatar != null)
        {
            //delete avatar
            await _avatarRepository.DeleteAvatarAsync(avatar);
        }
        //delete patientSpialist relations
        var patientSpecialists = patient.PatientSpecialists;
        foreach (var patientSpecialist in patientSpecialists)
        {
            await _patientSpecialistRepository.DeletePatientSpecialistAsync(patientSpecialist);
        }
        //delete patient
        await _patientRepository.DeletePatientAsync(patient);
    }
}