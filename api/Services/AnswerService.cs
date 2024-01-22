using Pebbles.Models;
using Pebbles.Repositories;
using Newtonsoft.Json;

namespace Pebbles.Services;

public interface IAnswerService
{
    Task ProcessAnswers(List<AnswerDTO> answers, Guid questionnaireId, int questionnaireIndex, IQuestionnaireService questionnaireService);
}

public class AnswerService : IAnswerService
{
    private readonly IAnswerRepository _answerRepository;
    private readonly IQuestionnaireRepository _questionnaireRepository;
    private readonly IQuestionRepository _questionRepository;
    private readonly IOptionRepository _optionRepository;

    public AnswerService(
        IAnswerRepository answerRepository,
        IQuestionnaireRepository questionnaireRepository,
        IQuestionRepository questionRepository,
        IOptionRepository optionRepository        )
    {
        _answerRepository = answerRepository;
        _questionnaireRepository = questionnaireRepository;
        _questionRepository = questionRepository;
        _optionRepository = optionRepository;
    }

    public async Task ProcessAnswers(List<AnswerDTO> answers, Guid questionnaireId, int questionnaireIndex, IQuestionnaireService questionnaireService)
    {
        try{
        Console.WriteLine($"Received questionnaireId: {questionnaireId}");


        var questionnaire = await _questionnaireRepository.GetQuestionnaireByIdAsync(questionnaireId);
        if (questionnaire == null)
        {
            Console.WriteLine($"Questionnaire with ID {questionnaireId} not found.");
            return; 
        }

        Console.WriteLine($"ProcessAnswers - Start: QuestionnaireId {questionnaireId}");

        questionnaire.Date = DateTime.Now;
        await _questionnaireRepository.UpdateQuestionnaireAsync(questionnaire);

        

        foreach (var answerDTO in answers)
        {
            var answerToSave = new Answer
            {
                Id = Guid.NewGuid(),
                QuestionId = answerDTO.QuestionId,
                OptionId = answerDTO.OptionId,
                QuestionnaireId = questionnaireId,
                QuestionnaireIndex = answerDTO.QuestionnaireIndex
            };

        
            Console.WriteLine($"ProcessAnswers - AnswerToSave: {JsonConvert.SerializeObject(answerToSave)}");


            await _answerRepository.AddAnswerAsync(answerToSave);

            Console.WriteLine($"ProcessAnswers - End: QuestionnaireId {questionnaireId}");
        }
        }
        catch(Exception ex)
        {
            Console.WriteLine($"ProcessAnswers - Exception: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
            }
            throw;
        }
    }
}

