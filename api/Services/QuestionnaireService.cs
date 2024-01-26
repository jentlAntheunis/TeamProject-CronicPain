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
    Task<bool> CheckIfBonusDone(Guid userId);

    Task<List<QuestionnaireDetailDTO>> GetQuestionnairesWithDetailsByPatientIdAsync(Guid patientId, List<string> categories);


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

  public async Task<bool> CheckIfBonusDone(Guid userId)
  {
    DateTime currentDate = DateTime.Now.Date;

    var questionnaireExists = await _context.Questionnaire
      .Include(q => q.Questions)
        .ThenInclude(q => q.Category)
      .Where(q => q.PatientId == userId)
      .Where(q => q.Date.HasValue && q.Date.Value.Date == currentDate)
      .AnyAsync(q => q.Questions.Any(q => q.Category.Name == "Bonus"));

    return questionnaireExists;
  }

  public async Task<List<QuestionnaireDetailDTO>> GetQuestionnairesWithDetailsByPatientIdAsync(Guid patientId, List<string> categories)
{
    var questionnaires = await _questionnaireRepository.GetFullQuestionnairesByPatientIdAsync(patientId);
    var detailedQuestionnaires = new List<QuestionnaireDetailDTO>();

    foreach (var questionnaire in questionnaires)
    {
        // Skip if Date is null
        if (questionnaire.Date == null) continue;

        var detailedQuestions = new List<QuestionDetailDTO>();
        string categoryName = null;

        foreach (var question in questionnaire.Questions)
        {
            if (!categories.Contains(question.Category.Name)) continue;

            if (categoryName == null)
            {
                categoryName = question.Category.Name;
            }

            var questionContent = await _context.Question.FirstOrDefaultAsync(q => q.Id == question.Id);

            var filteredAnswers = question.Answers
                .Where(a => a.QuestionnaireId == questionnaire.Id && (a.QuestionnaireIndex == 0 || a.QuestionnaireIndex == 1))
                .ToList();

            var optionIds = filteredAnswers.Select(a => a.OptionId).Distinct().ToList();
            var options = await _context.Option
                .Where(o => optionIds.Contains(o.Id))
                .ToListAsync();

            var answerDtos = filteredAnswers.Select(answer => new AnswerDTO
            {
                Id = answer.Id,
                QuestionId = answer.QuestionId,
                OptionId = answer.OptionId,
                OptionContent = options.FirstOrDefault(o => o.Id == answer.OptionId)?.Content,
                Position = options.FirstOrDefault(o => o.Id == answer.OptionId)?.Position, // Add Position here
                QuestionnaireIndex = answer.QuestionnaireIndex
            }).ToList();

            detailedQuestions.Add(new QuestionDetailDTO
            {
                Id = question.Id,
                Content = questionContent?.Content,
                Answers = answerDtos
            });
        }

        if (detailedQuestions.Any())
        {
            detailedQuestionnaires.Add(new QuestionnaireDetailDTO
            {
                Id = questionnaire.Id,
                CategoryName = categoryName,  // Use categoryName here
                Date = questionnaire.Date,
                PatientId = questionnaire.PatientId,
                Questions = detailedQuestions
            });
        }
    }

    return detailedQuestionnaires;
}



}
