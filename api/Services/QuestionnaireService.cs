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

    Task<List<QuestionnaireDetailDTO>> GetQuestionnairesWithDetailsByPatientIdAsync(Guid patientId);


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

    public async Task<List<QuestionnaireDetailDTO>> GetQuestionnairesWithDetailsByPatientIdAsync(Guid patientId)
    {
        var questionnaires = await _questionnaireRepository.GetFullQuestionnairesByPatientIdAsync(patientId);
        var detailedQuestionnaires = new List<QuestionnaireDetailDTO>();

        foreach (var questionnaire in questionnaires)
        {
            var detailedQuestions = new List<QuestionDetailDTO>();

            foreach (var question in questionnaire.Questions)
            {
                // Check if the question's category is "beweging" or "bonus"
                if (question.Category.Name == "beweging" || question.Category.Name == "bonus")
                {
                    var filteredAnswers = question.Answers
                        .Where(a => a.QuestionnaireId == questionnaire.Id && (a.QuestionnaireIndex == 0 || a.QuestionnaireIndex == 1))
                        .Select(a => new AnswerDTO
                        {
                            QuestionId = a.QuestionId,
                            OptionId = a.OptionId,
                            QuestionnaireIndex = a.QuestionnaireIndex
                        }).ToList();

                    detailedQuestions.Add(new QuestionDetailDTO
                    {
                        Id = question.Id,
                        Content = question.Content,
                        Answers = filteredAnswers
                    });
                }
            }

            // Add the questionnaire to the list only if it contains relevant questions
            if (detailedQuestions.Any())
            {
                detailedQuestionnaires.Add(new QuestionnaireDetailDTO
                {
                    Id = questionnaire.Id,
                    Date = questionnaire.Date,
                    Questions = detailedQuestions
                });
            }
        }

        return detailedQuestionnaires;
    }













}