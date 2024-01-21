using Pebbles.Models;
using Pebbles.Repositories;

namespace Pebbles.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public interface IQuestionnaireService
{
    Task<Questionnaire> GetQuestionnaireByIdAsync(Guid id);
    Task<List<Questionnaire>> GetQuestionnairesByPatientIdAsync(Guid id);
    Task<QuestionnaireDTO> AddMovementQuestionnaireAsync(Guid id);
    Task<QuestionnaireDTO> AddBonusQuestionnaireAsync(Guid userId);

    Task<QuestionDTO> AddDailyPainQuestionnaireAsync(Guid userId);
    Task<Questionnaire> UpdateQuestionnaireAsync(Questionnaire questionnaire);
    Task UpdateQuestionnaireIndexAsync(Guid questionnaireId, int questionnaireIndex);
    Task DeleteQuestionnaireAsync(Questionnaire questionnaire);
    Task<List<Questionnaire>> GetQuestionnairesAsync();
    Task<bool> CheckIfFirstQuestionnaireOfTheDay(Guid patientId);
}

public class QuestionnaireService : IQuestionnaireService
{
    private readonly IQuestionnaireRepository _questionnaireRepository;
    private readonly IQuestionRepository _questionRepository;
    private readonly IOptionRepository _optionRepository;
    private readonly IAnswerRepository _answerRepository;
    private readonly IAnswerService _answerService;

    public QuestionnaireService(
        IQuestionnaireRepository questionnaireRepository,
        IQuestionRepository questionRepository,
        IOptionRepository optionRepository,
        IAnswerRepository answerRepository,
        IAnswerService answerService
        )
    {
        _questionnaireRepository = questionnaireRepository;
        _questionRepository = questionRepository;
        _optionRepository = optionRepository;
        _answerRepository = answerRepository;
        _answerService = answerService;
    }

    public async Task<Questionnaire> GetQuestionnaireByIdAsync(Guid id) => await _questionnaireRepository.GetQuestionnaireByIdAsync(id);

    public async Task<List<Questionnaire>> GetQuestionnairesByPatientIdAsync(Guid id) => await _questionnaireRepository.GetQuestionnairesByPatientIdAsync(id);

    public async Task<QuestionnaireDTO> AddMovementQuestionnaireAsync(Guid id) => throw new NotImplementedException();

    public async Task<QuestionnaireDTO> AddBonusQuestionnaireAsync(Guid userId) => throw new NotImplementedException();
    
    public async Task<QuestionDTO> AddDailyPainQuestionnaireAsync(Guid userId) => throw new NotImplementedException();
    public async Task<Questionnaire> UpdateQuestionnaireAsync(Questionnaire questionnaire) => throw new NotImplementedException();
    public async Task UpdateQuestionnaireIndexAsync(Guid questionnaireId, int questionnaireIndex) => throw new NotImplementedException();
    public async Task DeleteQuestionnaireAsync(Questionnaire questionnaire) => throw new NotImplementedException();
    public async Task<List<Questionnaire>> GetQuestionnairesAsync() => throw new NotImplementedException();

    public async Task<bool> CheckIfFirstQuestionnaireOfTheDay(Guid patientId)
    {
        var questionnaires = await _questionnaireRepository.GetQuestionnairesByPatientIdAsync(patientId);

        if (questionnaires == null || questionnaires.Count == 0)
        {
            return true;
        }

        var today = DateTime.Now.Date;

        var hasIndex0Today = questionnaires.Any(q => q.Date.HasValue && q.Date.Value.Date == today && q.QuestionnaireIndex == 0);
        var hasIndex1Today = questionnaires.Any(q => q.Date.HasValue && q.Date.Value.Date == today && q.QuestionnaireIndex == 1);

        return !(hasIndex0Today && hasIndex1Today);
    }


}