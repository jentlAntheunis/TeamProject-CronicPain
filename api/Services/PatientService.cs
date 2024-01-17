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

    Task<Guid> StartMovementSessionAsync(Guid patientId);
    Task EndMovementSessionAsync(Guid movementSessionId);
    Task<List<MovementSuggestion>> GetMovementSuggestionsAsync(Guid patientId);
}

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;
    private readonly ISpecialistRepository _specialistRepository;
    private readonly IColorRepository _colorRepository;
    private readonly IMovementSessionRepository _movementSessionRepository;
    private readonly IMovementSuggestionRepository _movementSuggestionRepository;
    public PatientService(
        IPatientRepository patientRepository,
        ISpecialistRepository specialistRepository,
        IColorRepository colorRepository,
        IMovementSessionRepository movementSessionRepository,
        IMovementSuggestionRepository movementSuggestionRepository
        )
    {
        _patientRepository = patientRepository;
        _specialistRepository = specialistRepository;
        _colorRepository = colorRepository;
        _movementSessionRepository = movementSessionRepository;
        _movementSuggestionRepository = movementSuggestionRepository;
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

    public async Task<Guid> StartMovementSessionAsync(Guid patientId)
    {
        var patient = await _patientRepository.GetPatientByIdAsync(patientId);
        if (patient == null)
            throw new Exception("Patient does not exist");
        var movementSession = new MovementSession
        {
            PatientId = patient.Id,
            StartTime = DateTime.Now
        };
        return await _movementSessionRepository.CreateMovementSessionAsync(movementSession);
    }

    public async Task EndMovementSessionAsync(Guid movementSessionId)
    {
        var movementSession = await _movementSessionRepository.GetMovementSessionByIdAsync(movementSessionId);
        if (movementSession == null)
            throw new Exception("Movement session does not exist");
        movementSession.EndTime = DateTime.Now;
        movementSession.timeSpan = movementSession.EndTime - movementSession.StartTime;
        await _movementSessionRepository.UpdateMovementSessionAsync(movementSession);
    }

    public async Task<List<MovementSuggestion>> GetMovementSuggestionsAsync(Guid patientId)
    {
        var patient = await _patientRepository.GetPatientByIdAsync(patientId);
        if (patient == null)
            throw new Exception("Patient does not exist");
        var movementSuggestions = await _movementSuggestionRepository.GetMovementSuggestionsByPatientIdAsync(patientId);
        return movementSuggestions;
    }
}