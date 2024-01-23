using Pebbles.Models;
using Pebbles.Context;

namespace Pebbles.Repositories;

using Microsoft.EntityFrameworkCore;


public interface IOptionRepository
{
    Task<Option> GetOptionByIdAsync(Guid id);
    Task<Dictionary<Guid, int>> GetOptionScoresAsync();
    Task<IEnumerable<Option>> GetAllOptionsAsync();
    Task<Guid> CreateOptionAsync(Option option);
    Task<Option> UpdateOptionAsync(Option option);
}

public class OptionRepository : IOptionRepository
{
    private readonly PebblesContext _context;
    public OptionRepository(PebblesContext context)
    {
        _context = context;
    }

    public async Task<Option> GetOptionByIdAsync(Guid id) => await _context.Option.FindAsync(id);
    public async Task<Dictionary<Guid, int>> GetOptionScoresAsync()
    {
        var options = await _context.Option.ToListAsync();
        
        var scoresDictionary = new Dictionary<Guid, int>();

        foreach (var option in options)
        {
            if (int.TryParse(option.Position, out int position))
            {
                scoresDictionary[option.Id] = position;
            }
            else
            {
                // Handle the case where parsing the position as an integer fails.
                Console.WriteLine($"Failed to parse position {option.Position} as an integer.");
            }
        }

        return scoresDictionary;
    }



    public async Task<IEnumerable<Option>> GetAllOptionsAsync() => await Task.FromResult(_context.Option.ToList());

    public async Task<Guid> CreateOptionAsync(Option option)
    {
        await _context.Option.AddAsync(option);
        await _context.SaveChangesAsync();
        return option.Id;
    }

    public async Task<Option> UpdateOptionAsync(Option option)
    {
        _context.Option.Update(option);
        await _context.SaveChangesAsync();
        return option;
    }
}