using Pebbles.Models;
using Pebbles.Context;
using Microsoft.EntityFrameworkCore;

namespace Pebbles.Repositories;

public interface IQuestionnaireRepository
{
    Task<Questionnaire> GetQuestionnaireByIdAsync(Guid id);
    Task<Questionnaire> AddQuestionnaireAsync(Guid userId, string categoryName);
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

    public async Task<Questionnaire> AddQuestionnaireAsync(Guid userId, string categoryName)
    {
        var patient = await _userService.GetUserByIdAsync(userId);

        if (patient == null)
        {
            throw new Exception("Patient not found"); 
        }

        var category = await _context.Categories
            .Include(c => c.Questions) // Include the questions related to the category
            .FirstOrDefaultAsync(c => c.Name == categoryName);

        if (category == null)
        {
            throw new Exception("Category not found");
        }

        var questionnaire = new Questionnaire
        {
            Id = Guid.NewGuid(),
            Date = DateTime.Now,
            PatientId = userId
        };

        // Retrieve 5 random questions from the specified category
        var randomQuestions = category.Questions
            .OrderBy(q => Guid.NewGuid()) // Randomize the order
            .Take(5)
            .ToList();

        questionnaire.Questions = randomQuestions;

        _context.Questionnaires.Add(questionnaire);
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