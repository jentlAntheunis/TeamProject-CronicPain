using Google.Cloud.Firestore.V1;
using Newtonsoft.Json;
using Pebbles.Models;
using Pebbles.Repositories;
using Pebbles.Context;
using Microsoft.EntityFrameworkCore;

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
  Task<IntOverDaysDTO> GetMovementTimeWeekAsync(Guid patientId);
  Task<IntOverDaysDTO> GetStreakHistoryAsync(Guid patientId);
  Task<IntOverDaysDTO> GetPainHistoryAsync(Guid patientId);
  Task<bool> IsPatientLinkedToSpecialistAsync(Guid patientId, Guid specialistId);

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
  private readonly IQuestionnaireService _questionnaireService;
  private readonly ICategoryRepository _categoryRepository;
  private readonly IOptionRepository _optionRepository;

  private readonly PebblesContext _context;

  public PatientService(
      IPatientRepository patientRepository,
      ISpecialistRepository specialistRepository,
      IColorRepository colorRepository,
      IMovementSessionRepository movementSessionRepository,
      IMovementSuggestionRepository movementSuggestionRepository,
      IAvatarRepository avatarRepository,
      ILoginRepository loginRepository,
      IQuestionnaireRepository questionnaireRepository,
      ICategoryRepository categoryRepository,
      IQuestionnaireService questionnaireService,
      IOptionRepository optionRepository,
      PebblesContext context
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
    _categoryRepository = categoryRepository;
    _questionnaireService = questionnaireService;
    _optionRepository = optionRepository;
    _context = context;
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
    movementSession.StartTime = DateTime.Now;
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

  public async Task<IntOverDaysDTO> GetMovementTimeWeekAsync(Guid patientId)
  {
    var patient = await _patientRepository.GetPatientByIdAsync(patientId);
    if (patient == null)
      throw new Exception("Patient does not exist");
    var movementSessions = await _movementSessionRepository.GetMovementSessionsByPatientIdAsync(patientId);
    var IntOverDaysDTO = new IntOverDaysDTO
    {
      Days = movementSessions
            .Where(m => m.StartTime.Date >= DateTime.Now.AddDays(-7).Date)
            .GroupBy(m => m.StartTime.Date)
            .Select(g => new DayTDO
            {
              Date = g.Key,
              Int = g.Sum(m => m.Seconds)
            })
            .ToList()
    };
    return IntOverDaysDTO;
  }

  public async Task<IntOverDaysDTO> GetStreakHistoryAsync(Guid patientId)
  {
    var patient = await _patientRepository.GetPatientByIdAsync(patientId);
    if (patient == null)
      throw new Exception("Patient does not exist");
    var questionnaires = await _questionnaireRepository.GetQuestionnairesByPatientIdAsync(patientId);
    var IntOverDaysDTO = new IntOverDaysDTO
    {
      Days = questionnaires
            .Where(q => q.Date.HasValue && q.Date.Value.Date >= DateTime.Now.AddDays(-7).Date)
            .GroupBy(q => q.Date.Value.Date)
            .Select(g => new DayTDO
            {
              Date = g.Key,
              Int = g.Count()
            })
            .ToList()
    };
    return IntOverDaysDTO;
  }

  public async Task<IntOverDaysDTO> GetPainHistoryAsync(Guid patientId)
  {
      var categories = new List<string>() { "pijn" };
      var questionnaires = await _questionnaireService.GetQuestionnairesWithDetailsByPatientIdAsync(patientId, categories);

      questionnaires = questionnaires
          .Where(q => q.Date.HasValue && q.Date.Value.Date >= DateTime.Now.AddDays(-30).Date)
          .OrderBy(q => q.Date)
          .ToList();

      var intOverDaysTDO = new IntOverDaysDTO
      {
          Days = new List<DayTDO>()
      };

      foreach (var questionnaire in questionnaires)
      {
          var question = questionnaire.Questions.FirstOrDefault();
          var answer = question.Answers.FirstOrDefault();
          var option = await _optionRepository.GetOptionByIdAsync(answer.OptionId);
          var dayTDO = new DayTDO
          {
              Date = questionnaire.Date.Value.Date,
              Int = int.TryParse(option.Position, out int result) ? result : 0
          };
          intOverDaysTDO.Days.Add(dayTDO);
      }

      return intOverDaysTDO;
  }

  public async Task<bool> IsPatientLinkedToSpecialistAsync(Guid patientId, Guid specialistId)
  {
    return await _context.PatientSpecialist
        .AnyAsync(ps => ps.PatientId == patientId && ps.SpecialistId == specialistId);
  }

}
