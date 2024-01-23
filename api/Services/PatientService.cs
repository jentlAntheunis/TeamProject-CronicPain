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
    Task AddPatientListToSpecialistAsync(Guid SpecialistId, List<Patient> patients);
    Task<Patient> UpdatePatientAsync(Patient patient);
    Task AddCoinsAsync(Guid patientId, int amount);
    Task<MovementSession> AddMovementSessionAsync(Guid patientId, MovementSession movementSession);
    Task<List<MovementSession>> GetMovementSessionsAsync(Guid patientId);
    Task<List<MovementSuggestion>> GetMovementSuggestionsAsync(Guid patientId);
    Task AddMovementSuggestion(Guid specialistId, Guid patientId, MovementSuggestion movementSuggestionId);
    Task<string> GetPebblesMoodAsync(Guid patientId);
    Task CheckStreakAsync(Guid patientId);
    Task<WeekDTO> GetMovementTimeWeekAsync(Guid patientId);
    Task<WeekDTO> GetStreakHistoryAsync(Guid patientId);
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
    private readonly IQuestionnaireRepository _questionnaireRepository;
    public PatientService(
        IPatientRepository patientRepository,
        ISpecialistRepository specialistRepository,
        IColorRepository colorRepository,
        IMovementSessionRepository movementSessionRepository,
        IMovementSuggestionRepository movementSuggestionRepository,
        IAvatarRepository avatarRepository,
        ILoginRepository loginRepository,
        IQuestionnaireRepository questionnaireRepository
        )
    {
        _patientRepository = patientRepository;
        _specialistRepository = specialistRepository;
        _colorRepository = colorRepository;
        _movementSessionRepository = movementSessionRepository;
        _movementSuggestionRepository = movementSuggestionRepository;
        _avatarRepository = avatarRepository;
        _loginRepository = loginRepository;
        _questionnaireRepository = questionnaireRepository;
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

        var patients = await _patientRepository.GetAllPatientsAsync();

        //check by email, then lastname, then firstname
        var existingPatient = patients
            .Where(p => p.Email == patient.Email)
            .Where(p => p.LastName == patient.LastName)
            .Where(p => p.FirstName == patient.FirstName)
            .FirstOrDefault();
        if (existingPatient != null)
        {
            existingPatient.PatientSpecialists.Add(new PatientSpecialist { PatientId = existingPatient.Id, SpecialistId = SpecialistId });
            await _patientRepository.UpdatePatientAsync(existingPatient);
            return patient.Id;
        }
        patient.PatientSpecialists.Add(new PatientSpecialist { PatientId = patient.Id, SpecialistId = SpecialistId });
        var color = await _colorRepository.GetDefaultColorAsync();
        patient.Colors.Add(color);
        patient.Avatar.ColorId = color.Id;
        return await _patientRepository.CreatePatientAsync(patient);
    }

    public async Task AddPatientListToSpecialistAsync(Guid SpecialistId, List<Patient> patients)
    {
        foreach (var patient in patients)
        {
            await AddPatientBySpecialistAsync(SpecialistId, patient);
        }
        return;
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
        if (patient.Streak >= 3) return "HAPPY";

        var logins = await _loginRepository.GetLoginsByUserAsync(patientId);
        var firstLogin = logins.OrderBy(l => l.Timestamp).FirstOrDefault();
        var isNewPatient = firstLogin.Timestamp > DateTime.Now.AddDays(-3);

        var questionnaires = await _questionnaireRepository.GetQuestionnairesByPatientIdAsync(patientId);
        var questionnairesByDate = questionnaires
            .Where(q => q.Date.HasValue)
            .GroupBy(q => q.Date.Value.Date)
            .Select(g => g.First())
            .ToList();
        if (!isNewPatient)
        {
            if (questionnairesByDate.Count >= 3) return "HAPPY";
            if (questionnairesByDate.Count >= 1) return "NEUTRAL";
            return "SAD";
        }
        if (questionnairesByDate.Count >= 1) return "HAPPY";
        return "NEUTRAL";
    }

    public async Task AddCoinsAsync(Guid patientId, int amount)
    {
        var patient = await _patientRepository.GetPatientByIdAsync(patientId);
        if (patient == null)
            throw new Exception("Patient does not exist");
        patient.Coins += amount;
        await _patientRepository.UpdatePatientAsync(patient);
    }

    public async Task CheckStreakAsync(Guid patientId)
    {
        var patient = await _patientRepository.GetPatientByIdAsync(patientId);
        if (patient == null)
            throw new Exception("Patient does not exist");
        var questionnaires = await _questionnaireRepository.GetQuestionnairesByPatientIdAsync(patientId);
        var questionnairesYesterday = questionnaires.Where(q => q.Date.HasValue && q.Date.Value.Date == DateTime.Now.AddDays(-1).Date);
        var questionnairesToday = questionnaires.Where(q => q.Date.HasValue && q.Date.Value.Date == DateTime.Now.Date);
        if (!questionnairesYesterday.Any() && !questionnairesToday.Any())
        {
            patient.Streak = 0;
            await _patientRepository.UpdatePatientAsync(patient);
            return;
        }
    }

    public async Task<WeekDTO> GetMovementTimeWeekAsync(Guid patientId)
    {
        var patient = await _patientRepository.GetPatientByIdAsync(patientId);
        if (patient == null)
            throw new Exception("Patient does not exist");
        var movementSessions = await _movementSessionRepository.GetMovementSessionsByPatientIdAsync(patientId);
        var weekDTO = new WeekDTO
        {
            Days = movementSessions
                .Where(m => m.StartTime.Date >= DateTime.Now.AddDays(-7).Date)
                .GroupBy(m => m.StartTime.Date)
                .Select(g => new DayTDO
                {
                    Date = g.Key,
                    Total = g.Sum(m => m.Seconds)
                })
                .ToList()
        };
        return weekDTO;
    }

    public async Task<WeekDTO> GetStreakHistoryAsync(Guid patientId)
    {
        var patient = await _patientRepository.GetPatientByIdAsync(patientId);
        if (patient == null)
            throw new Exception("Patient does not exist");
        var questionnaires = await _questionnaireRepository.GetQuestionnairesByPatientIdAsync(patientId);
        var weekDTO = new WeekDTO
        {
            Days = questionnaires
                .Where(q => q.Date.HasValue)
                .GroupBy(q => q.Date.Value.Date)
                .Select(g => new DayTDO
                {
                    Date = g.Key,
                    Total = g.Count()
                })
                .ToList()
        };
        return weekDTO;
    }
}