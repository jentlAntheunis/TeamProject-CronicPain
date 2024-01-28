using Pebbles.Models;
using Pebbles.Context;
using Microsoft.EntityFrameworkCore;

namespace Pebbles.Repositories;

public interface IQuestionRepository
{
    Task<List<Question>> GetAllQuestionsAsync();
    Task<Question> GetQuestionByIdAsync(Guid id);
    Task<Guid> CreateQuestionAsync(Question question);
    Task<Question> UpdateQuestionAsync(Question question);
    Task DeleteQuestionAsync(Question question);
}

public class QuestionRepository : IQuestionRepository
{
    private readonly PebblesContext _context;

    public QuestionRepository(PebblesContext context)
    {
        _context = context;
    }

    public async Task<List<Question>> GetAllQuestionsAsync() =>
        await _context.Question.Where(q => !q.IsDeleted).ToListAsync();

    public async Task<Question> GetQuestionByIdAsync(Guid id) => await _context.Question.FirstOrDefaultAsync(q => q.Id == id);

    public async Task<Guid> CreateQuestionAsync(Question question)
    {
        await _context.Question.AddAsync(question);
        await _context.SaveChangesAsync();
        return question.Id;
    }

    public async Task<Question> UpdateQuestionAsync(Question question)
    {
        _context.Question.Update(question);
        await _context.SaveChangesAsync();
        return question;
    }

    public async Task DeleteQuestionAsync(Question question)
    {
        question.IsDeleted = true;
        question.DeletedAt = DateTimeOffset.UtcNow;
        _context.Question.Update(question);
        await _context.SaveChangesAsync();
    }

}
