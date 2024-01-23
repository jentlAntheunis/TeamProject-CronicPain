using Pebbles.Models;
using Pebbles.Repositories;
using Newtonsoft.Json;

namespace Pebbles.Services;

public interface IAnswerService
{
    Task ProcessAnswers(List<AnswerDTO> answers, Guid questionnaireId, int questionnaireIndex, IQuestionnaireService questionnaireService);
    Task<MovementImpact> CompareAnswersAndCalculateScore(Guid questionnaireId);
    Task<Dictionary<Guid, MovementImpact>> GetQuestionnaireImpactsByUserId(Guid userId);


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

    public async Task<MovementImpact> CompareAnswersAndCalculateScore(Guid questionnaireId)
    {
        var beforeAnswers = await _answerRepository.GetAnswersByQuestionnaireIdAndIndex(questionnaireId, 0);
        var afterAnswers = await _answerRepository.GetAnswersByQuestionnaireIdAndIndex(questionnaireId, 1);

        int totalScore = 0;
        foreach(var beforeAnswer in beforeAnswers)
        {
            var afterAnswer = afterAnswers.FirstOrDefault(a => a.QuestionId == beforeAnswer.QuestionId);
            if(afterAnswer != null)
            {
                int score = CompareAnswerPair(beforeAnswer, afterAnswer);
                totalScore += score;
            }
        }

        return DetermineMovementImpact(totalScore);
    }

    private int CompareAnswerPair(Answer beforeAnswer, Answer afterAnswer)
    {
        // Map OptionId to scores (1-4)
        var optionScores = new Dictionary<Guid, int>
        {
            { new Guid("first-option-id"), 1 },
            { new Guid("second-option-id"), 2 },
            { new Guid("third-option-id"), 3 },
            { new Guid("fourth-option-id"), 4 }
        };

        int beforeScore = optionScores[beforeAnswer.OptionId];
        int afterScore = optionScores[afterAnswer.OptionId];

        // Scoring logic: Each point of improvement gets a score of +1
        int score = afterScore - beforeScore;

        return score;
    }

    public enum MovementImpact
    {
        Negative,
        Neutral,
        Positive
    }

    public MovementImpact DetermineMovementImpact(int totalScore)
    {
        // Define thresholds
        const int NegativeThreshold = -5;
        const int PositiveThreshold = 5;

        if (totalScore < NegativeThreshold)
            return MovementImpact.Negative;
        else if (totalScore > PositiveThreshold)
            return MovementImpact.Positive;
        else
            return MovementImpact.Neutral;
    }

    public async Task<Dictionary<Guid, MovementImpact>> GetQuestionnaireImpactsByUserId(Guid userId)
    {
        var questionnaireIds = await _questionnaireRepository.GetQuestionnaireIdsByUserId(userId);

        var impacts = new Dictionary<Guid, MovementImpact>();
        foreach (var questionnaireId in questionnaireIds)
        {
            var impact = await CompareAnswersAndCalculateScore(questionnaireId);
            impacts.Add(questionnaireId, impact);
        }

        return impacts;
    }

    public async Task<Dictionary<Guid, MovementImpact>> GetQuestionnaireImpactsByUserId(Guid userId)
    {
        // Logic to fetch all questionnaire IDs for the given user ID
        var questionnaireIds = await _questionnaireRepository.GetQuestionnaireIdsByUserId(userId);

        var impacts = new Dictionary<Guid, MovementImpact>();
        foreach (var questionnaireId in questionnaireIds)
        {
            var impact = await CompareAnswersAndCalculateScore(questionnaireId);
            impacts.Add(questionnaireId, impact);
        }

        return impacts;
    }






}

