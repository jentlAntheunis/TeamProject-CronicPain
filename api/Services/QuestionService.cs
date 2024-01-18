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

    public QuestionService(IQuestionRepository questionRepository, ICategoryRepository categoryRepository)
    {
        _questionRepository = questionRepository;
        _categoryRepository = categoryRepository;
    }
    

    public async Task<List<Question>> GetAllQuestionsAsync() => await _questionRepository.GetAllQuestionsAsync();

    public async Task<Question> GetQuestionByIdAsync(Guid id) => await _questionRepository.GetQuestionByIdAsync(id);

    public async Task<Guid> CreateQuestionAsync(Question question)
        {
            // Fetch the category name from the database using the CategoryId
            string categoryName = await _categoryRepository.GetCategoryNameByIdAsync(question.CategoryId);

            // Define a mapping between categoryName and ScaleName
            Dictionary<string, string> categoryToScaleMapping = new Dictionary<string, string>
            {
                { "beweging", "oneens_eens" },
                { "bonus", "niet_altijd" },
            };

            // Check if categoryName exists in the mapping, and set ScaleName accordingly
            if (categoryToScaleMapping.ContainsKey(categoryName))
            {
                question.ScaleName = categoryToScaleMapping[categoryName];
            }
            else
            {
                question.ScaleName = "oneens_eens"; // 
            }

            return await _questionRepository.CreateQuestionAsync(question);
        }

    public async Task<Question> UpdateQuestionAsync(Question question) => await _questionRepository.UpdateQuestionAsync(question);

    public async Task DeleteQuestionAsync(Question question) => await _questionRepository.DeleteQuestionAsync(question);
}

