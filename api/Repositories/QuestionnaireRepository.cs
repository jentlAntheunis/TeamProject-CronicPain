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

    Task<QuestionDTO> AddDailyPainQuestionnaireAsync(Guid userId);
    Task<Questionnaire> UpdateQuestionnaireAsync(Questionnaire questionnaire);
    Task UpdateQuestionnaireIndexAsync(Guid questionnaireId, int questionnaireIndex);
    Task DeleteQuestionnaireAsync(Questionnaire questionnaire);
    Task<List<Questionnaire>> GetQuestionnairesAsync();
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
            PatientId = patientId
        };

        try
        {
            var categoryId = await _context.Category
                .Where(c => c.Name == "beweging")
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            if (categoryId == Guid.Empty)
            {
                throw new InvalidOperationException("Category 'beweging' not found.");
            }

            var randomQuestions = await _context.Question
                .Where(q => q.CategoryId == categoryId)
                .OrderBy(q => Guid.NewGuid()) // Shuffle the questions randomly
                .Take(5)
                .Include(q => q.Scale) 
                .ThenInclude(scale => scale.Options)
                .ToListAsync();

            var questionnaireQuestions = randomQuestions.Select(question => new QuestionnaireQuestion
            {
                QuestionnaireId = questionnaire.Id,
                QuestionId = question.Id
            }).ToList();

            await _context.Questionnaire.AddAsync(questionnaire);
            await _context.QuestionnaireQuestion.AddRangeAsync(questionnaireQuestions);

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
        
        var questionnaire = new Questionnaire
        {
            Id= Guid.NewGuid(),
            PatientId = userId,
            Date = null
        };

        var categoryId = await _context.Category
        .Where(c => c.Name == "bonus")
        .Select(c => c.Id)
        .FirstOrDefaultAsync();


        var randomQuestions = await _context.Question
        .Where(q => q.CategoryId == categoryId)
        .OrderBy(q => Guid.NewGuid()) // Shuffle the questions randomly
        .Take(5)
        .Include(q => q.Scale) 
        .ThenInclude(scale => scale.Options)
        .ToListAsync();

        foreach (var question in randomQuestions)
        {
            var questionnaireQuestion = new QuestionnaireQuestion
            {
                QuestionnaireId = questionnaire.Id,
                QuestionId = question.Id
            };

            await _context.QuestionnaireQuestion.AddAsync(questionnaireQuestion);
        }
            
        await _context.Questionnaire.AddAsync(questionnaire);
        await _context.SaveChangesAsync();

        // Map the created Questionnaire to QuestionnaireDTO (using AutoMapper)
        var questionnaireDTO = _mapper.Map<QuestionnaireDTO>(questionnaire);

        return questionnaireDTO;
    }

    public async Task<QuestionDTO> AddDailyPainQuestionnaireAsync(Guid userId)
    {
        var questionnaire = new Questionnaire
        {
            Id = Guid.NewGuid(),
            PatientId = userId,
            Date = null
        };

        var categoryId = await _context.Category
            .Where(c => c.Name == "pijn")
            .Select(c => c.Id)
            .FirstOrDefaultAsync();

        var question = await _context.Question
            .Where(q => q.CategoryId == categoryId && q.Scale.Name == "1_10")
            .Include(q => q.Scale)
            .ThenInclude(scale => scale.Options)
            .FirstOrDefaultAsync();

        if (question != null)
        {
            var questionnaireQuestion = new QuestionnaireQuestion
            {
                QuestionnaireId = questionnaire.Id,
                QuestionId = question.Id
            };

            await _context.QuestionnaireQuestion.AddAsync(questionnaireQuestion);
            await _context.Questionnaire.AddAsync(questionnaire);
            await _context.SaveChangesAsync();

            // Map the selected Question to QuestionDTO (using AutoMapper)
            var questionDTO = _mapper.Map<QuestionDTO>(question);
            return questionDTO;
        }

        // Handle the case where no question is found
        return null;
    }

    

    public async Task<Questionnaire> UpdateQuestionnaireAsync(Questionnaire questionnaire)
    {
        _context.Questionnaire.Update(questionnaire);
        await _context.SaveChangesAsync();
        return questionnaire;
    }

    public async Task UpdateQuestionnaireIndexAsync(Guid questionnaireId, int questionnaireIndex)
    {
        var questionnaire = await _context.Questionnaire.SingleOrDefaultAsync(q => q.Id == questionnaireId);

        if (questionnaire != null)
        {
            questionnaire.QuestionnaireIndex = questionnaireIndex;
            await _context.SaveChangesAsync();
        }
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
}