using Pebbles.Models;
using Pebbles.Repositories;

namespace Pebbles.Services;

public interface IQuestionnaireService
{
    Task<Questionnaire> AddQuestionnaireAsync(Guid userId, string categoryName);
    Task<Questionnaire> GetQuestionnaireByPatientIdAsync(Guid id);
    Task<Questionnaire> GetQuestionnaireAsync(Guid id);
    Task<List<Questionnaire>> GetQuestionnairesAsync();
    Task<Questionnaire> UpdateQuestionnaireAsync(Questionnaire questionnaire);
    Task DeleteQuestionnaireAsync(Guid id);
}

public class QuestionnaireService : IQuestionnaireService
{
    private readonly IQuestionnaireRepository _questionnaireRepository;
    private readonly IUserService _userService;

    public QuestionnaireService(
        IQuestionnaireRepository questionnaireRepository,
        IUserService userService
        )
    {
        _questionnaireRepository = questionnaireRepository;
        _userService = userService;
    }

    public async Task<Questionnaire> AddQuestionnaireAsync(Guid userId, string categoryName) => await _questionnaireRepository.AddQuestionnaireAsync(userId, categoryName);

    public async Task<Questionnaire> GetQuestionnaireByPatientIdAsync(Guid id) => await _questionnaireRepository.GetQuestionnaireByPatientIdAsync(id);

    public async Task<Questionnaire> GetQuestionnaireAsync(Guid id) => await _questionnaireRepository.GetQuestionnaireByIdAsync(id);

    public async Task<List<Questionnaire>> GetQuestionnairesAsync() => await _questionnaireRepository.GetQuestionnairesAsync();

    public async Task<Questionnaire> UpdateQuestionnaireAsync(Questionnaire questionnaire) => await _questionnaireRepository.UpdateQuestionnaireAsync(questionnaire);

    public async Task DeleteQuestionnaireAsync(Guid id)
    {
        var questionnaire = await _questionnaireRepository.GetQuestionnaireByIdAsync(id);
        await _questionnaireRepository.DeleteQuestionnaireAsync(questionnaire);
    }
}

