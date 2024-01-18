using Pebbles.Models;
using Pebbles.Context;
using Microsoft.EntityFrameworkCore;

namespace Pebbles.Repositories;

public interface IQuestionnaireRepository
{
    Task<Questionnaire> GetQuestionnaireByIdAsync(Guid id);
    Task<Questionnaire> GetQuestionnaireByPatientIdAsync(Guid id);
    Task<Questionnaire> AddMovementQuestionnaireAsync(Guid id);
    Task<Questionnaire> AddBonusQuestionnaireAsync(Guid userId);
    Task<Questionnaire> UpdateQuestionnaireAsync(Questionnaire questionnaire);
    Task DeleteQuestionnaireAsync(Questionnaire questionnaire);
    Task<List<Questionnaire>> GetQuestionnairesAsync();
}

public class QuestionnaireRepository : IQuestionnaireRepository
{
    private readonly PebblesContext _context;

    public QuestionnaireRepository(PebblesContext context)
    {
        _context = context;
    }

    public async Task<Questionnaire> GetQuestionnaireByIdAsync(Guid id) => await _context.Questionnaire.FirstOrDefaultAsync(q => q.Id == id);

    public async Task<Questionnaire> GetQuestionnaireByPatientIdAsync(Guid id) => await _context.Questionnaire.FirstOrDefaultAsync(q => q.PatientId == id);

    public async Task<Questionnaire> AddMovementQuestionnaireAsync(Guid id)
    {
        var questionnaire = new Questionnaire
        {
            Id= Guid.NewGuid(),
            PatientId = id,
        };

        //Retrieve the category ID based on the provided category name
        var categoryId = await _context.Category
        .Where(c => c.Name == "beweging")
        .Select(c => c.Id)
        .FirstOrDefaultAsync();

        if (categoryId == null)
        {
            // Handle the case where the category name does not exist
            throw new ArgumentException("Category not found.");
        }

        // Retrieve a list of random question IDs from the specified category
        var randomQuestions = await _context.Question
        .Where(q => q.CategoryId == categoryId)
        .OrderBy(q => Guid.NewGuid()) // Shuffle the questions randomly
        .Take(5)
        .Include(q => q.Scale) // Include the scale
        .ThenInclude(scale => scale.Options) // Include the options for the scale
        .ToListAsync();

        // Step 3: Create QuestionnaireQuestion objects for selected questions
        foreach (var question in randomQuestions)
        {
            var questionnaireQuestion = new QuestionnaireQuestion
            {
                QuestionnaireId = questionnaire.Id,
                QuestionId = question.Id
            };

            // Add the questionnaire item to the context (not saving yet)
            await _context.QuestionnaireQuestion.AddAsync(questionnaireQuestion);
        }
            
        await _context.Questionnaire.AddAsync(questionnaire);
        await _context.SaveChangesAsync();
        return questionnaire;
    }

    public async Task<Questionnaire> AddBonusQuestionnaireAsync(Guid userId)
    {
        var questionnaire = new Questionnaire
        {
            Id= Guid.NewGuid(),
            PatientId = userId,
        };

        //Retrieve the category ID based on the provided category name
        var categoryId = await _context.Category
        .Where(c => c.Name == "bonus")
        .Select(c => c.Id)
        .FirstOrDefaultAsync();

        if (categoryId == null)
        {
            // Handle the case where the category name does not exist
            throw new ArgumentException("Category not found.");
        }

        // Retrieve a list of random question IDs from the specified category
        var randomQuestions = await _context.Question
        .Where(q => q.CategoryId == categoryId)
        .OrderBy(q => Guid.NewGuid()) // Shuffle the questions randomly
        .Take(5)
        .Include(q => q.Scale) // Include the scale
        .ThenInclude(scale => scale.Options) // Include the options for the scale
        .ToListAsync();

        // Step 3: Create QuestionnaireQuestion objects for selected questions
        foreach (var question in randomQuestions)
        {
            var questionnaireQuestion = new QuestionnaireQuestion
            {
                QuestionnaireId = questionnaire.Id,
                QuestionId = question.Id
            };

            // Add the questionnaire item to the context (not saving yet)
            await _context.QuestionnaireQuestion.AddAsync(questionnaireQuestion);
        }
            
        await _context.Questionnaire.AddAsync(questionnaire);
        await _context.SaveChangesAsync();
        return questionnaire;
    }

    public async Task<Questionnaire> UpdateQuestionnaireAsync(Questionnaire questionnaire)
    {
        _context.Questionnaire.Update(questionnaire);
        await _context.SaveChangesAsync();
        return questionnaire;
    }

    public async Task DeleteQuestionnaireAsync(Questionnaire questionnaire)
    {
        //check if questionnaire has answers
        var answers = await _context.Answer.Where(a => a.QuestionnaireId == questionnaire.Id).ToListAsync();
        if (answers != null)
        {
            //delete answers
            _context.Answer.RemoveRange(answers);
        }
        //delete questionnaire
        _context.Questionnaire.Remove(questionnaire);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Questionnaire>> GetQuestionnairesAsync() => await _context.Questionnaire.ToListAsync();
}