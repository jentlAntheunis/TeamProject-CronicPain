using Pebbles.Models;
using Pebbles.Context;
using Microsoft.EntityFrameworkCore;

namespace Pebbles.Repositories;

public interface IScaleRepository
{
    Task<List<Scale>> GetAllScalesAsync();
    Task<Scale> GetScaleByIdAsync(Guid id);
    Task<string> GetScaleNameByIdAsync(Guid id);
    Task<Guid> GetScaleIdByNameAsync(string name);
    Task<Guid> CreateScaleAsync(Scale scale);
    Task<Scale> UpdateScaleAsync(Scale scale);
    Task DeleteScaleAsync(Scale scale);
}

public class ScaleRepository : IScaleRepository
{
    private readonly PebblesContext _context;

    public ScaleRepository(PebblesContext context)
    {
        _context = context;
    }

    public async Task<List<Scale>> GetAllScalesAsync() => await _context.Scale.ToListAsync();

    public async Task<Scale> GetScaleByIdAsync(Guid id) => await _context.Scale.FirstOrDefaultAsync(s => s.Id == id);

    public async Task<Guid> GetScaleIdByNameAsync(string name) => await _context.Scale.Where(s => s.Name == name).Select(s => s.Id).FirstOrDefaultAsync();

    public async Task<string> GetScaleNameByIdAsync(Guid id) => await _context.Scale.Where(s => s.Id == id).Select(s => s.Name).FirstOrDefaultAsync();

    public async Task<Guid> CreateScaleAsync(Scale scale)
    {
        await _context.Scale.AddAsync(scale);
        await _context.SaveChangesAsync();
        return scale.Id;
    }

    public async Task<Scale> UpdateScaleAsync(Scale scale)
    {
        _context.Scale.Update(scale);
        await _context.SaveChangesAsync();
        return scale;
    }

    public async Task DeleteScaleAsync(Scale scale)
    {
        //check if scale has options
        var options = await _context.Option.Where(o => o.ScaleId == scale.Id).ToListAsync();
        if (options != null)
        {
            //delete options
            _context.Option.RemoveRange(options);
        }
        //check if scale has questions
        var questions = await _context.Question.Where(q => q.ScaleId == scale.Id).ToListAsync();
        if (questions != null)
        {
            //delete questions
            _context.Question.RemoveRange(questions);
        }
        //delete scale
        _context.Scale.Remove(scale);
        await _context.SaveChangesAsync();
    }
}