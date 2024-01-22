using Pebbles.Models;
using Pebbles.Context;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


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

    public async Task<Answer> GetAnswerByIdAsync(Guid id) => await _context.Answer.FirstOrDefaultAsync(a => a.Id == id);

    public async Task<Answer> AddAnswerAsync(Answer answer)
    {
        await _context.Answer.AddAsync(answer);
        await _context.SaveChangesAsync();

        // Log the saved answer
        Console.WriteLine($"Saved Answer: {JsonConvert.SerializeObject(answer)}");

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