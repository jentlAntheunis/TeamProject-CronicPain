using Pebbles.Models;
using Pebbles.Repositories;
using Pebbles.Context;

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

    Task<QuestionnaireDTO> AddDailyPainQuestionnaireAsync(Guid userId);
    Task<Questionnaire> UpdateQuestionnaireAsync(Questionnaire questionnaire);
    Task UpdateQuestionnaireIndexAsync(Guid questionnaireId, int questionnaireIndex);
    Task DeleteQuestionnaireAsync(Questionnaire questionnaire);
    Task<List<Questionnaire>> GetQuestionnairesAsync();

    Task<bool> CheckIfFirstQuestionnaireOfTheDay(Guid userId);

}

public class QuestionnaireService : IQuestionnaireService
{
    private readonly IQuestionnaireRepository _questionnaireRepository;
    private readonly IQuestionRepository _questionRepository;
    private readonly IOptionRepository _optionRepository;
    private readonly IAnswerRepository _answerRepository;
    private readonly IAnswerService _answerService;

    private readonly PebblesContext _context;

    public QuestionnaireService(
        IQuestionnaireRepository questionnaireRepository,
        IQuestionRepository questionRepository,
        IOptionRepository optionRepository,
        IAnswerRepository answerRepository,
        IAnswerService answerService,
        PebblesContext context
        )
    {
        _questionnaireRepository = questionnaireRepository;
        _questionRepository = questionRepository;
        _optionRepository = optionRepository;
        _answerRepository = answerRepository;
        _answerService = answerService;
        _context = context;
    }

    public async Task<Questionnaire> GetQuestionnaireByIdAsync(Guid id) => await _questionnaireRepository.GetQuestionnaireByIdAsync(id);

    public async Task<List<Questionnaire>> GetQuestionnairesByPatientIdAsync(Guid id) => await _questionnaireRepository.GetQuestionnairesByPatientIdAsync(id);

    public async Task<QuestionnaireDTO> AddMovementQuestionnaireAsync(Guid id) => throw new NotImplementedException();

    public async Task<QuestionnaireDTO> AddBonusQuestionnaireAsync(Guid userId) => throw new NotImplementedException();

    public async Task<QuestionnaireDTO> AddDailyPainQuestionnaireAsync(Guid userId) => throw new NotImplementedException();
    public async Task<Questionnaire> UpdateQuestionnaireAsync(Questionnaire questionnaire) => throw new NotImplementedException();
    public async Task UpdateQuestionnaireIndexAsync(Guid questionnaireId, int questionnaireIndex) => throw new NotImplementedException();
    public async Task DeleteQuestionnaireAsync(Questionnaire questionnaire) => throw new NotImplementedException();
    public async Task<List<Questionnaire>> GetQuestionnairesAsync() => throw new NotImplementedException();


    public async Task<bool> CheckIfFirstQuestionnaireOfTheDay(Guid userId)
    {
        DateTime currentDate = DateTime.Now.Date;

        var questionnaireExists = await _context.Questionnaire
            .AnyAsync(q => q.PatientId == userId && q.Date.HasValue && q.Date.Value.Date == currentDate);

        bool isFirstQuestionnaire = !questionnaireExists;
        if (isFirstQuestionnaire)
        {
            var patient = await _context.Patient
                .FirstOrDefaultAsync(p => p.Id == userId);

            patient.Streak += 1;
            await _context.SaveChangesAsync();
        }
        return isFirstQuestionnaire;
    }









}