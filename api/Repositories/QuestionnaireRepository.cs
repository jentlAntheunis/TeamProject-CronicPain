using Microsoft.EntityFrameworkCore;
using AutoMapper;

using Pebbles.Models;
using Pebbles.Context;

namespace Pebbles.Repositories;

public interface IQuestionnaireRepository
{
  Task<Questionnaire> GetQuestionnaireByIdAsync(Guid id);
  Task<List<Questionnaire>> GetQuestionnairesByPatientIdAsync(Guid id);
  Task<QuestionnaireDTO> AddMovementQuestionnaireAsync(Guid id);
  Task<QuestionnaireDTO> AddBonusQuestionnaireAsync(Guid userId);
  Task<QuestionnaireDTO> AddDailyPainQuestionnaireAsync(Guid userId);
  Task<Questionnaire> UpdateQuestionnaireAsync(Questionnaire questionnaire);
  Task DeleteQuestionnaireAsync(Questionnaire questionnaire);
  Task<List<Questionnaire>> GetQuestionnairesAsync();
  Task<List<Guid>> GetQuestionnaireIdsByUserId(Guid userId);
  Task<List<Questionnaire>> GetFullQuestionnairesByPatientIdAsync(Guid patientId);
  Task<List<Guid>> GetQuestionnaireIdsByUserIdAndCategory(Guid userId, string categoryName);

}

public class QuestionnaireRepository : IQuestionnaireRepository
{
  private readonly PebblesContext _context;
  private readonly IMapper _mapper;

  public QuestionnaireRepository(PebblesContext context, IMapper mapper)
  {
    _context = context;
    _mapper = mapper;
  }

  public async Task<Questionnaire> GetQuestionnaireByIdAsync(Guid id) => await _context.Questionnaire.FirstOrDefaultAsync(q => q.Id == id);

  public async Task<List<Questionnaire>> GetQuestionnairesByPatientIdAsync(Guid id) => await _context.Questionnaire.Where(q => q.PatientId == id).ToListAsync();

  public async Task<QuestionnaireDTO> AddMovementQuestionnaireAsync(Guid patientId)
  {
    Console.WriteLine($"AddMovementQuestionnaireAsync - Start: PatientId {patientId}");

    var questionnaire = new Questionnaire
    {
      Id = Guid.NewGuid(),
      PatientId = patientId,
      Date = null
    };

    try
    {
      var categoryName = "beweging";
      var scaleName = "oneens_eens";

      var categoryId = await _context.Category
          .Where(c => c.Name == categoryName)
          .Select(c => c.Id)
          .FirstOrDefaultAsync();

      var scaleId = await _context.Scale
          .Where(s => s.Name == scaleName)
          .Select(s => s.Id)
          .FirstOrDefaultAsync();


      Console.WriteLine($"AddMovementQuestionnaireAsync - CategoryId: {categoryId}");

      var randomQuestions = await _context.Question
          .Where(q => q.CategoryId == categoryId)
          .OrderBy(q => Guid.NewGuid()) // Shuffle the questions randomly
          .Take(5)
          .Include(q => q.Scale)
          .ThenInclude(scale => scale.Options)
          .ToListAsync();

      Console.WriteLine($"AddMovementQuestionnaireAsync - RandomQuestions: {randomQuestions.Count}");

      await _context.Questionnaire.AddAsync(questionnaire);
      await _context.SaveChangesAsync();

      foreach (var question in randomQuestions)
      {
        var option = await _context.Option
            .Where(o => o.ScaleId == scaleId)
            .FirstOrDefaultAsync();

        if (option == null)
        {
          throw new InvalidOperationException($"Option scale '{scaleName}' not found.");
        }

        var questionnaireQuestion = new QuestionnaireQuestion
        {
          QuestionnaireId = questionnaire.Id,
          QuestionId = question.Id
        };

        await _context.QuestionnaireQuestion.AddAsync(questionnaireQuestion);
      }

      await _context.SaveChangesAsync();

      // Map the created Questionnaire to QuestionnaireDTO (using AutoMapper)
      var questionnaireDTO = _mapper.Map<QuestionnaireDTO>(questionnaire);

      Console.WriteLine($"AddMovementQuestionnaireAsync - Completed: QuestionnaireId {questionnaire.Id}");

      return questionnaireDTO;
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error during AddMovementQuestionnaireAsync: {ex.Message}");
      if (ex.InnerException != null)
      {
        Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
      }
      throw;
    }
  }

  public async Task<QuestionnaireDTO> AddBonusQuestionnaireAsync(Guid userId)
  {
    Console.WriteLine($"AddBonusQuestionnaireAsync - Start: UserId {userId}");

    var questionnaire = new Questionnaire
    {
      Id = Guid.NewGuid(),
      PatientId = userId,
      Date = null
    };

    try
    {
      var categoryName = "bonus";
      var scaleName = "niet_altijd";

      var categoryId = await _context.Category
          .Where(c => c.Name == categoryName)
          .Select(c => c.Id)
          .FirstOrDefaultAsync();

      var scaleId = await _context.Scale
          .Where(s => s.Name == scaleName)
          .Select(s => s.Id)
          .FirstOrDefaultAsync();

      if (categoryId == Guid.Empty)
      {
        throw new InvalidOperationException($"Category '{categoryName}' not found.");
      }

      Console.WriteLine($"AddBonusQuestionnaireAsync - CategoryId: {categoryId}");

      var randomQuestions = await _context.Question
          .Where(q => q.CategoryId == categoryId)
          .OrderBy(q => Guid.NewGuid()) // Shuffle the questions randomly
          .Take(10)
          .Include(q => q.Scale)
          .ThenInclude(scale => scale.Options)
          .ToListAsync();

      Console.WriteLine($"AddBonusQuestionnaireAsync - RandomQuestions: {randomQuestions.Count}");

      await _context.Questionnaire.AddAsync(questionnaire);
      await _context.SaveChangesAsync();

      foreach (var question in randomQuestions)
      {
        var option = await _context.Option
            .Where(o => o.ScaleId == scaleId)
            .FirstOrDefaultAsync();

        if (option == null)
        {
          throw new InvalidOperationException($"Option scale '{scaleName}' not found.");
        }

        var questionnaireQuestion = new QuestionnaireQuestion
        {
          QuestionnaireId = questionnaire.Id,
          QuestionId = question.Id
        };

        await _context.QuestionnaireQuestion.AddAsync(questionnaireQuestion);
      }


      await _context.SaveChangesAsync();

      // Map the created Questionnaire to QuestionnaireDTO (using AutoMapper)
      var questionnaireDTO = _mapper.Map<QuestionnaireDTO>(questionnaire);

      Console.WriteLine($"AddBonusQuestionnaireAsync - Completed: QuestionnaireId {questionnaire.Id}");

      return questionnaireDTO;
    }

    catch (Exception ex)
    {
      Console.WriteLine($"Error during AddMovementQuestionnaireAsync: {ex.Message}");
      if (ex.InnerException != null)
      {
        Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
      }
      throw;
    }

  }




  public async Task<QuestionnaireDTO> AddDailyPainQuestionnaireAsync(Guid userId)
  {
    var questionnaire = new Questionnaire
    {
      Id = Guid.NewGuid(),
      PatientId = userId,
      Date = null
    };

    await _context.Questionnaire.AddAsync(questionnaire);

    await _context.SaveChangesAsync();

    try
    {
      var categoryName = "pijn";
      var scaleName = "0_10";

      var categoryId = await _context.Category
          .Where(c => c.Name == categoryName)
          .Select(c => c.Id)
          .FirstOrDefaultAsync();

      var scaleId = await _context.Scale
          .Where(s => s.Name == scaleName)
          .Select(s => s.Id)
          .FirstOrDefaultAsync();


      Console.WriteLine($"AddDailyPainQuestionnaireAsync - CategoryId: {categoryId}");

      var questions = await _context.Question
          .Where(q => q.CategoryId == categoryId)
          .Include(q => q.Scale)
          .ThenInclude(scale => scale.Options)
          .ToListAsync();

      Console.WriteLine($"AddDailyPainQuestionnaireAsync - Questions Count: {questions.Count}");

      foreach (var question in questions)
      {
        var option = await _context.Option
            .Where(o => o.ScaleId == scaleId)
            .FirstOrDefaultAsync();

        if (option == null)
        {
          throw new InvalidOperationException($"Option '{scaleName}' not found in category '{categoryName}'.");
        }

        var existingQuestionnaire = await _context.Questionnaire.FindAsync(questionnaire.Id);


        var questionnaireQuestion = new QuestionnaireQuestion
        {
          QuestionnaireId = questionnaire.Id,
          QuestionId = question.Id
        };

        await _context.QuestionnaireQuestion.AddAsync(questionnaireQuestion);
      }

      await _context.SaveChangesAsync();

      // Map the created Questionnaire to QuestionnaireDTO (using AutoMapper)
      var questionnaireDTO = _mapper.Map<QuestionnaireDTO>(questionnaire);

      Console.WriteLine($"AddDailyPainQuestionnaireAsync - Completed: QuestionnaireId {questionnaire.Id}");

      return questionnaireDTO;
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error during AddDailyPainQuestionnaireAsync: {ex.Message}");
      if (ex.InnerException != null)
      {
        Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
      }
      throw;
    }
  }

  public async Task<Questionnaire> UpdateQuestionnaireAsync(Questionnaire questionnaire)
  {
    _context.Questionnaire.Update(questionnaire);
    await _context.SaveChangesAsync();
    return questionnaire;
  }



  public async Task DeleteQuestionnaireAsync(Questionnaire questionnaire)
  {
    var answers = await _context.Answer.Where(a => a.QuestionnaireId == questionnaire.Id).ToListAsync();
    if (answers != null)
    {

      _context.Answer.RemoveRange(answers);
    }
    _context.Questionnaire.Remove(questionnaire);
    await _context.SaveChangesAsync();
  }

  public async Task<List<Questionnaire>> GetQuestionnairesAsync() => await _context.Questionnaire.ToListAsync();

  public async Task<List<Guid>> GetQuestionnaireIdsByUserId(Guid userId)
  {
    var questionnaireIds = await _context.Questionnaire
        .Where(q => q.PatientId == userId)
        .Select(q => q.Id)
        .ToListAsync();

    return questionnaireIds;
  }
  public async Task<List<Questionnaire>> GetFullQuestionnairesByPatientIdAsync(Guid patientId)
  {
    var questionnaires = await _context.Questionnaire
        .Where(q => q.PatientId == patientId)
        .Include(q => q.Questions)
            .ThenInclude(question => question.Answers)
        .ToListAsync();

    foreach (var questionnaire in questionnaires)
    {
      foreach (var question in questionnaire.Questions)
      {
        // Eagerly load the Category for each Question
        question.Category = await _context.Category
            .FirstOrDefaultAsync(c => c.Id == question.CategoryId);
      }
    }

    return questionnaires;
  }

  public async Task<List<Guid>> GetQuestionnaireIdsByUserIdAndCategory(Guid userId, string categoryName)
  {
    var categoryId = await _context.Category
        .Where(c => c.Name == categoryName)
        .Select(c => c.Id)
        .FirstOrDefaultAsync();

    if (categoryId == Guid.Empty)
    {
      throw new InvalidOperationException($"Category '{categoryName}' not found.");
    }

    var questionnaireIds = await _context.Questionnaire
        .Where(q => q.PatientId == userId)
        .Join(_context.QuestionnaireQuestion,
            questionnaire => questionnaire.Id,
            questionnaireQuestion => questionnaireQuestion.QuestionnaireId,
            (questionnaire, questionnaireQuestion) => new { questionnaire, questionnaireQuestion })
        .Join(_context.Question,
            joint => joint.questionnaireQuestion.QuestionId,
            question => question.Id,
            (joint, question) => new { joint.questionnaire, question })
        .Where(joint => joint.question.CategoryId == categoryId)
        .Select(joint => joint.questionnaire.Id)
        .Distinct()
        .ToListAsync();

    return questionnaireIds;
  }



}
