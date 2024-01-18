using Pebbles.Repositories;
using Pebbles.Models;

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

    public async Task<Answer> GetAnswerByIdAsync(Guid id) => await _answerRepository.GetAnswerByIdAsync(id);

    public async Task<Answer> AddAnswerAsync(Answer answer)
    {
        answer.QuestionId = new Guid(); //dummy data
        answer.QuestionnaireId = new Guid(); //dummy data
        answer.OptionId = new Guid();    //dummy data

        return await _answerRepository.AddAnswerAsync(answer);
    }
    public async Task<Answer> UpdateAnswerAsync(Answer answer) => await _answerRepository.UpdateAnswerAsync(answer);

    public async Task DeleteAnswerAsync(Answer answer) => await _answerRepository.DeleteAnswerAsync(answer);
}