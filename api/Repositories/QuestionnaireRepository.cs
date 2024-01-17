using Pebbles.Models;
using Pebbles.Context;
using Microsoft.EntityFrameworkCore;

namespace Pebbles.Repositories;

public interface IQuestionnaireRepository
{
    Task<Questionnaire> GetQuestionnaireByIdAsync(Guid id);
    Task<Questionnaire> AddQuestionnaireAsync(string email);
    Task<Questionnaire> UpdateQuestionnaireAsync(Questionnaire questionnaire);
    Task DeleteQuestionnaireAsync(Questionnaire questionnaire);
}

public class QuestionnaireRepository : IQuestionnaireRepository
{
    private readonly PebblesContext _context;

    public QuestionnaireRepository(PebblesContext context)
    {
        _context = context;
    }

    public async Task<Questionnaire> GetQuestionnaireByIdAsync(Guid id) => await _context.Questionnaire.FirstOrDefaultAsync(q => q.Id == id);

    public async Task<Questionnaire> AddQuestionnaireAsync(string email)
    {
        // Get the user (patient) by email
        var patient = await _userService.GetUserByEmailAsync(email);

        if (patient == null)
        {
            throw new Exception("Patient not found");
        }

        var questionnaire = new Questionnaire
        {
            Id = Guid.NewGuid(),
            Date = DateTime.Now,
            PatientId = patient.Id
        };

        // Retrieve 5 random questions from the database
        var randomQuestions = await _context.Questions
            .OrderBy(q => Guid.NewGuid()) // Randomize the order
            .Take(5)
            .ToListAsync();

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
        //check if questionnaire has questions
        var questions = await _context.Question.Where(q => q.QuestionnaireId == questionnaire.Id).ToListAsync();
        if (questions != null)
        {
            //delete questions
            _context.Question.RemoveRange(questions);
        }
        //delete questionnaire
        _context.Questionnaire.Remove(questionnaire);
        await _context.SaveChangesAsync();
    }
}