using Microsoft.EntityFrameworkCore;
using AutoMapper;

using Pebbles.Models;
using Pebbles.Context;

namespace Pebbles.Repositories;

public interface IQuestionnaireRepository
{
    Task<Questionnaire> GetQuestionnaireByIdAsync(Guid id);
    Task<Questionnaire> GetQuestionnaireByPatientIdAsync(Guid id);
    Task<QuestionnaireDTO> AddMovementQuestionnaireAsync(Guid id);
    Task<QuestionnaireDTO> AddBonusQuestionnaireAsync(Guid userId);
    Task<Questionnaire> UpdateQuestionnaireAsync(Questionnaire questionnaire);
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

    public async Task<Questionnaire> GetQuestionnaireByPatientIdAsync(Guid id) => await _context.Questionnaire.FirstOrDefaultAsync(q => q.PatientId == id);


    public async Task<QuestionnaireDTO> AddMovementQuestionnaireAsync(Guid patientId)
    {
        Console.WriteLine($"AddMovementQuestionnaireAsync - Start: PatientId {patientId}");

        // Create a new Questionnaire object
        var questionnaire = new Questionnaire
        {
            Id = Guid.NewGuid(),
            PatientId = patientId
        };

        try
        {
            // Retrieve the category ID for "beweging"
            var categoryId = await _context.Category
                .Where(c => c.Name == "beweging")
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            if (categoryId == Guid.Empty)
            {
                throw new InvalidOperationException("Category 'beweging' not found.");
            }

            // Retrieve a list of random questions from the specified category
            var randomQuestions = await _context.Question
                .Where(q => q.CategoryId == categoryId)
                .OrderBy(q => Guid.NewGuid()) // Shuffle the questions randomly
                .Take(5)
                .Include(q => q.Scale) // Include the scale
                .ThenInclude(scale => scale.Options) // Include the options for the scale
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