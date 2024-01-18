using Pebbles.Models;
using Pebbles.Context;

namespace Pebbles.Repositories;

public interface IOptionRepository
{
    Task<Option> GetOptionByIdAsync(Guid id);
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