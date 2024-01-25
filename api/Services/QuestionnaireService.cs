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
  // Task<QuestionnaireDTO> AddMovementQuestionnaireAsync(Guid id);
  // Task<QuestionnaireDTO> AddBonusQuestionnaireAsync(Guid userId);
  // Task<QuestionnaireDTO> AddDailyPainQuestionnaireAsync(Guid userId);
  // Task<Questionnaire> UpdateQuestionnaireAsync(Questionnaire questionnaire);
  // Task UpdateQuestionnaireIndexAsync(Guid questionnaireId, int questionnaireIndex);
  // Task DeleteQuestionnaireAsync(Questionnaire questionnaire);
  // Task<List<Questionnaire>> GetQuestionnairesAsync();

  Task<bool> CheckIfFirstQuestionnaireOfTheDay(Guid userId);
  Task<bool> CheckIfFirstBonusOfTheDay(Guid userId);

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

  // public async Task<QuestionnaireDTO> AddMovementQuestionnaireAsync(Guid id) => throw new NotImplementedException();
  // public async Task<QuestionnaireDTO> AddBonusQuestionnaireAsync(Guid userId) => throw new NotImplementedException();
  // public async Task<QuestionnaireDTO> AddDailyPainQuestionnaireAsync(Guid userId) => throw new NotImplementedException();
  // public async Task<Questionnaire> UpdateQuestionnaireAsync(Questionnaire questionnaire) => throw new NotImplementedException();
  // public async Task UpdateQuestionnaireIndexAsync(Guid questionnaireId, int questionnaireIndex) => throw new NotImplementedException();
  // public async Task DeleteQuestionnaireAsync(Questionnaire questionnaire) => throw new NotImplementedException();
  // public async Task<List<Questionnaire>> GetQuestionnairesAsync() => throw new NotImplementedException();


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

  public async Task<bool> CheckIfFirstBonusOfTheDay(Guid userId)
  {
    DateTime currentDate = DateTime.Now.Date;

    var questionnaireExists = await _context.Questionnaire
        .Include(q => q.Questions)
            .ThenInclude(q => q.Category)
        .Where(q => q.Questions.Any(q => q.Category.Name == "bonus"))
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

  public async Task<List<QuestionnaireDetailDTO>> GetQuestionnairesWithDetailsByPatientIdAsync(Guid patientId, List<string> categories)
  {
    try
    {
      var questionnaires = await _questionnaireRepository.GetFullQuestionnairesByPatientIdAsync(patientId);

      if (questionnaires == null || !questionnaires.Any())
      {
        Console.WriteLine($"No questionnaires found for patientId: {patientId}");
        return new List<QuestionnaireDetailDTO>();
      }

      var detailedQuestionnaires = new List<QuestionnaireDetailDTO>();

      foreach (var questionnaire in questionnaires.Where(q => q.Date != null))
      {
        var firstQuestionCategory = questionnaire.Questions.FirstOrDefault()?.Category;
        var questionnaireCategory = await _context.Category.FirstOrDefaultAsync(c => c.Id == firstQuestionCategory.Id);

        var detailedQuestions = new List<QuestionDetailDTO>();

        foreach (var question in questionnaire.Questions)
        {
          var questionCategory = await _context.Category.FirstOrDefaultAsync(c => c.Id == question.CategoryId);
          if (questionCategory != null && categories.Contains(questionCategory.Name))
          {
            var filteredAnswers = question.Answers
                .Select(a =>
                {
                  var option = _context.Option.FirstOrDefault(o => o.Id == a.OptionId);
                  return new AnswerDTO
                  {
                    QuestionId = a.QuestionId,
                    OptionId = a.OptionId,
                    OptionContent = option?.Content,
                    QuestionnaireIndex = a.QuestionnaireIndex
                  };
                }).ToList();

            detailedQuestions.Add(new QuestionDetailDTO
            {
              Id = question.Id,
              Content = question.Content,
              Answers = filteredAnswers
            });
          }
        }

        if (detailedQuestions.Any())
        {
          detailedQuestionnaires.Add(new QuestionnaireDetailDTO
          {
            Id = questionnaire.Id,
            Date = questionnaire.Date,
            PatientId = questionnaire.PatientId,
            CategoryName = questionnaireCategory?.Name,
            Questions = detailedQuestions
          });
        }
      }

      return detailedQuestionnaires;
    }
    catch(Exception ex)
    {
      Console.WriteLine(ex);
      throw;
    }
  }
}
