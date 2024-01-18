using Pebbles.Repositories;

namespace Pebbles.Services;

public interface IAnswerService
{
    Task<Answer> GetAnswerByIdAsync(Guid id);
    Task<Answer> AddAnswerAsync(Answer answer);
    Task<Answer> UpdateAnswerAsync(Answer answer);
    Task DeleteAnswerAsync(Answer answer);
}

public class AnswerService : IAnswerService
{
    private readonly IAnswerRepository _answerRepository;

    public AnswerService(IAnswerRepository answerRepository)
    {
        _answerRepository = answerRepository;
    }

    public async Task<Answer> AddAnswerAsync(Answer answer)
    {
        answer.QuestionId = someQuestionId;
        answer.QuestionnaireId = someQuestionnaireId;
        answer.OptionId = someOptionId;

        return await _answerRepository.AddAnswerAsync(answer);
    }
    public async Task<Answer> AddAnswerAsync(Answer answer) => await _answerRepository.AddAnswerAsync(answer);

    public async Task<Answer> UpdateAnswerAsync(Answer answer) => await _answerRepository.UpdateAnswerAsync(answer);

    public async Task DeleteAnswerAsync(Answer answer) => await _answerRepository.DeleteAnswerAsync(answer);
}