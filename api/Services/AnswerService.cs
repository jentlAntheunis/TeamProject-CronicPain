using Pebbles.Models;
using Pebbles.Repositories;

namespace Pebbles.Services;

public interface IAnswerService
{
    Task ProcessAnswers(List<AnswerDTO> answers, Guid questionnaireId, int questionnaireIndex);
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
        IOptionRepository optionRepository
        )
    {
        _answerRepository = answerRepository;
        _questionnaireRepository = questionnaireRepository;
        _questionRepository = questionRepository;
        _optionRepository = optionRepository;
    }

    public async Task ProcessAnswers(List<AnswerDTO> answers, Guid questionnaireId, int questionnaireIndex)
    {
        foreach (var answerDTO in answers)
        {
            var answerToSave = new Answer
            {
                Id = Guid.NewGuid(),
                QuestionId = answerDTO.QuestionId,
                OptionId = answerDTO.OptionId,
                QuestionnaireId = questionnaireId
            };

            await _answerRepository.AddAnswerAsync(answerToSave);

            await _questionnaireRepository.UpdateQuestionnaireIndexAsync(questionnaireId, questionnaireIndex);
        }
    }
}

