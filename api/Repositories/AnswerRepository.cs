using Pebbles.Models;
using Pebbles.Context;
using Microsoft.EntityFrameworkCore;

namespace Pebbles.Repositories;

public interface IAnswerRepository
{
    Task<Answer> GetAnswerByIdAsync(Guid id);
    Task<Answer> AddAnswerAsync(Answer answer);
    Task<Answer> UpdateAnswerAsync(Answer answer);
    Task DeleteAnswerAsync(Answer answer);
}

public class AnswerRepository : IAnswerRepository
{
    private readonly PebblesContext _context;

    public AnswerRepository(PebblesContext context)
    {
        _context = context;
    }

    public async Task<Answer> GetAnswerByIdAsync(Guid id)
    {
        return await _context.Answer
            .Include(a => a.Question)
            .Include(a => a.Questionnaire)
            .Include(a => a.Option)
            .FirstOrDefaultAsync(a => a.Id == id);
    }
    public async Task<Answer> AddAnswerAsync(Answer answer)
    {
        await _context.Answer.AddAsync(answer);
        await _context.SaveChangesAsync();
        return answer;
    }

    public async Task<Answer> UpdateAnswerAsync(Answer answer)
    {
        _context.Answer.Update(answer);
        await _context.SaveChangesAsync();
        return answer;
    }

    public async Task DeleteAnswerAsync(Answer answer)
    {
        _context.Answer.Remove(answer);
        await _context.SaveChangesAsync();
    }
}