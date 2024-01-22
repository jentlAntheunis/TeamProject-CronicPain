using Newtonsoft.Json;
using Pebbles.Models;
using Pebbles.Repositories;

namespace Pebbles.Services;

public interface IPatientService
{
    Task<Patient> GetPatientByIdAsync(Guid id);
    Task<IEnumerable<Patient>> GetPatientsBySpecialistAsync(Guid SpecialistId);
    Task<Patient> GetPatientDetailsByIdAsync(Guid id);
    Task<Guid> AddPatientBySpecialistAsync(Guid SpecialistId, Patient patient);
    Task AddPatientToSpecialistAsync(Guid PatientId, Guid SpecialistId);
    Task<Patient> UpdatePatientAsync(Patient patient);
    Task AddCoinsAsync(Guid patientId, int amount);
    Task<MovementSession> AddMovementSessionAsync(Guid patientId, MovementSession movementSession);
    Task<List<MovementSession>> GetMovementSessionsAsync(Guid patientId);
    Task<List<MovementSuggestion>> GetMovementSuggestionsAsync(Guid patientId);
    Task AddMovementSuggestion(Guid specialistId, Guid patientId, MovementSuggestion movementSuggestionId);
    Task<string> GetPebblesMoodAsync(Guid patientId);
}

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;
    private readonly ISpecialistRepository _specialistRepository;
    private readonly IColorRepository _colorRepository;
    private readonly IMovementSessionRepository _movementSessionRepository;
    private readonly IMovementSuggestionRepository _movementSuggestionRepository;
    private readonly IAvatarRepository _avatarRepository;
    private readonly ILoginRepository _loginRepository;
    public PatientService(
        IPatientRepository patientRepository,
        ISpecialistRepository specialistRepository,
        IColorRepository colorRepository,
        IMovementSessionRepository movementSessionRepository,
        IMovementSuggestionRepository movementSuggestionRepository,
        IAvatarRepository avatarRepository,
        ILoginRepository loginRepository
        )
    {
        _patientRepository = patientRepository;
        _specialistRepository = specialistRepository;
        _colorRepository = colorRepository;
        _movementSessionRepository = movementSessionRepository;
        _movementSuggestionRepository = movementSuggestionRepository;
        _avatarRepository = avatarRepository;
        _loginRepository = loginRepository;
    }

    public async Task<Patient> GetPatientByIdAsync(Guid id) => await _patientRepository.GetPatientByIdAsync(id);

    public async Task<IEnumerable<Patient>> GetPatientsBySpecialistAsync(Guid SpecialistId) => await _patientRepository.GetPatientsBySpecialistIdAsync(SpecialistId);

    public async Task<Patient> GetPatientDetailsByIdAsync(Guid id)
    {
        var patient = await _patientRepository.GetPatientDetailsByIdAsync(id);
        Console.WriteLine(JsonConvert.SerializeObject(patient));
        if (patient == null)
            throw new Exception("Patient does not exist");
        return patient;
    }

    public async Task<Guid> AddPatientBySpecialistAsync(Guid SpecialistId, Patient patient)
    {
        var specialist = await _specialistRepository.GetSpecialistByIdAsync(SpecialistId);
        if (specialist == null)
            throw new Exception("Specialist does not exist");
        patient.PatientSpecialists.Add(new PatientSpecialist { PatientId = patient.Id, SpecialistId = SpecialistId });

        var color = await _colorRepository.GetDefaultColorAsync();
        patient.Colors.Add(color);
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

    public async Task<MovementSession> AddMovementSessionAsync(Guid patientId, MovementSession movementSession)
    {
        movementSession.PatientId = patientId;
        await _movementSessionRepository.CreateMovementSessionAsync(movementSession);
        return movementSession;
    }

    public async Task<List<MovementSession>> GetMovementSessionsAsync(Guid patientId)
    {
        var patient = await _patientRepository.GetPatientByIdAsync(patientId);
        if (patient == null)
            throw new Exception("Patient does not exist");
        var movementSessions = await _movementSessionRepository.GetMovementSessionsByPatientIdAsync(patientId);
        return movementSessions;
    }

    public async Task<List<MovementSuggestion>> GetMovementSuggestionsAsync(Guid patientId)
    {
        var patient = await _patientRepository.GetPatientByIdAsync(patientId);
        if (patient == null)
            throw new Exception("Patient does not exist");
        var movementSuggestions = await _movementSuggestionRepository.GetMovementSuggestionsByPatientIdAsync(patientId);
        return movementSuggestions;
    }

    public async Task AddMovementSuggestion(Guid specialistId, Guid patientId, MovementSuggestion movementSuggestion)
    {
        var patient = await _patientRepository.GetPatientByIdAsync(patientId);
        if (patient == null)
            throw new Exception("Patient does not exist");
        var specialist = await _specialistRepository.GetSpecialistByIdAsync(specialistId);
        if (specialist == null)
            throw new Exception("Specialist does not exist");
        movementSuggestion.PatientId = patientId;
        movementSuggestion.SpecialistId = specialistId;
        await _movementSuggestionRepository.CreateMovementSuggestionAsync(movementSuggestion);
    }

    public async Task<string> GetPebblesMoodAsync(Guid patientId)
    {
        var patient = await _patientRepository.GetPatientByIdAsync(patientId);
        
        if (patient == null)
            throw new Exception("Patient does not exist");

        //look at past 5 days and see when patient has logged in
        Console.WriteLine(JsonConvert.SerializeObject(patient));
        string mood = "happy";
        return mood;
    }

    public async Task AddCoinsAsync(Guid patientId, int amount)
    {
        var patient = await _patientRepository.GetPatientByIdAsync(patientId);
        if (patient == null)
            throw new Exception("Patient does not exist");
        patient.Coins += amount;
        await _patientRepository.UpdatePatientAsync(patient);
    }
}