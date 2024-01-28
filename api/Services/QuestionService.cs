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
    private readonly ICategoryRepository _categoryRepository;
    private readonly IScaleRepository _scaleRepository;

    public QuestionService(IQuestionRepository questionRepository, ICategoryRepository categoryRepository, IScaleRepository scaleRepository)
    {
        _questionRepository = questionRepository;
        _categoryRepository = categoryRepository;
        _scaleRepository = scaleRepository;
    }


    public async Task<List<Question>> GetAllQuestionsAsync() => await _questionRepository.GetAllQuestionsAsync();

    public async Task<Question> GetQuestionByIdAsync(Guid id) => await _questionRepository.GetQuestionByIdAsync(id);

    public async Task<Guid> CreateQuestionAsync(Question question)
    {
        string categoryName = await _categoryRepository.GetCategoryNameByIdAsync(question.CategoryId);

        string scaleName = await _scaleRepository.GetScaleNameByIdAsync(question.ScaleId);

        if (categoryName == "beweging")
        {
            question.ScaleId = await _scaleRepository.GetScaleIdByNameAsync("oneens_eens");
        }
        else if (categoryName == "bonus")
        {
            question.ScaleId = await _scaleRepository.GetScaleIdByNameAsync("niet_altijd");
        }

        return await _questionRepository.CreateQuestionAsync(question);
    }




    public async Task<Question> UpdateQuestionAsync(Question question) => await _questionRepository.UpdateQuestionAsync(question);

    public async Task DeleteQuestionAsync(Question question) =>
            await _questionRepository.DeleteQuestionAsync(question);

    }

