using Pebbles.Models;
using Pebbles.Repositories;

namespace Pebbles.Services;

public interface IQuestionService
{
    Task<List<Question>> GetAllQuestionsAsync();
    Task<Question> GetQuestionByIdAsync(Guid id);
    Task<Guid> CreateQuestionAsync(Question question);
    Task<Question> UpdateQuestionAsync(Question question);
    Task DeleteQuestionAsync(Question question);
}

public class QuestionService : IQuestionService
{
    private readonly IQuestionRepository _questionRepository;

    public QuestionService(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    public async Task<List<Question>> GetAllQuestionsAsync() => await _questionRepository.GetAllQuestionsAsync();

    public async Task<Question> GetQuestionByIdAsync(Guid id) => await _questionRepository.GetQuestionByIdAsync(id);

    public async Task<Guid> CreateQuestionAsync(Question question)
        {
            if (question.CategoryName == "beweging")
            {
                question.ScaleName = "oneens_eens";
            }
            else if (question.CategoryName == "bonus")
            {
                question.ScaleName = "niet_altijd";
            }

        return await _questionRepository.CreateQuestionAsync(question);
        }

    public async Task<Question> UpdateQuestionAsync(Question question) => await _questionRepository.UpdateQuestionAsync(question);

    public async Task DeleteQuestionAsync(Question question) => await _questionRepository.DeleteQuestionAsync(question);
}

