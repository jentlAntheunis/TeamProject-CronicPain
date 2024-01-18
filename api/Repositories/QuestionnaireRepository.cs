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

/*
    public async Task<Questionnaire> AddMovementQuestionnaireAsync(Guid id)
    {
        
        Console.WriteLine($"AddMovementQuestionnaireAsync - Start: PatientId {id}");

        var questionnaire = new Questionnaire
        {
            Id= Guid.NewGuid(),
            PatientId = id
        };

        //Retrieve the category ID based on the provided category name
        var categoryId = await _context.Category
        .Where(c => c.Name == "beweging")
        .Select(c => c.Id)
        .FirstOrDefaultAsync();


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

            Console.WriteLine($"Adding QuestionnaireQuestion - QuestionnaireId: {questionnaireQuestion.QuestionnaireId}, QuestionId: {questionnaireQuestion.QuestionId}");


            // Add the questionnaire item to the context (not saving yet)
            await _context.QuestionnaireQuestion.AddAsync(questionnaireQuestion);
        }
            
        await _context.Questionnaire.AddAsync(questionnaire);

        // Log entity states
        foreach (var entry in _context.ChangeTracker.Entries())
        {
            Console.WriteLine($"Entity: {entry.Entity.GetType().Name}, State: {entry.State}");
        }

        foreach (var entry in _context.ChangeTracker.Entries())
        {
            if (entry.State == EntityState.Unchanged)
            {
                entry.State = EntityState.Detached;
            }
        }

        foreach (var entry in _context.ChangeTracker.Entries<Question>())
        {
            entry.State = EntityState.Detached;
        }


        try
        {
            Console.WriteLine("AddMovementQuestionnaireAsync - Before SaveChangesAsync");
            await _context.SaveChangesAsync();
            Console.WriteLine($"AddMovementQuestionnaireAsync - Completed: QuestionnaireId {questionnaire.Id}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during SaveChangesAsync: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
            }
            throw; // Rethrow the exception to maintain existing behavior
        }



        Console.WriteLine("AddBonusQuestionnaireAsync - Completed");
        return questionnaire;
                
    }
*/
    

    public async Task<Questionnaire> AddMovementQuestionnaireAsync(Guid patientId)
{
    Console.WriteLine($"AddSimpleQuestionnaireAsync - Start: PatientId {patientId}");

    // Create a basic Questionnaire entity
    var questionnaire = new Questionnaire
    {
        Id = Guid.NewGuid(),
        PatientId = patientId
        // Do not set any other properties or relationships
    };

    // Add the entity to the context
    _context.Questionnaire.Add(questionnaire);
    

    try
    {
        Console.WriteLine("AddSimpleQuestionnaireAsync - Before SaveChangesAsync");
        await _context.SaveChangesAsync();
        Console.WriteLine($"AddSimpleQuestionnaireAsync - Completed: QuestionnaireId {questionnaire.Id}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error during SaveChangesAsync: {ex.Message}");
        if (ex.InnerException != null)
        {
            Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
        }
        throw; // Rethrow the exception
    }

    return questionnaire;
}



    public async Task<Questionnaire> AddBonusQuestionnaireAsync(Guid userId)
    {
        /*
        var questionnaire = new Questionnaire
        {
            Id= Guid.NewGuid(),
            PatientId = userId,
            Date = null
        };

        //Retrieve the category ID based on the provided category name
        var categoryId = await _context.Category
        .Where(c => c.Name == "bonus")
        .Select(c => c.Id)
        .FirstOrDefaultAsync();


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
        */
        throw new NotImplementedException();
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