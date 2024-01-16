using Pebbles.Models;
using Pebbles.Context;
using Microsoft.EntityFrameworkCore;

namespace Pebbles.Repositories;

public interface IColorRepository
{
    Task<List<Color>> GetAllColorsAsync();
    Task<Color> GetColorByIdAsync(Guid id);
    Task<Color> GetDefaultColorAsync();
    Task<Color> CreateColorAsync(Color color);
    Task<Color> UpdateColorAsync(Color color);
    Task DeleteColorAsync(Color color);
}

public class ColorRepository : IColorRepository
{
    private readonly PebblesContext _context;

    public ColorRepository(IConfiguration configuration)
    {
        _context = new PebblesContext(configuration);
    }

    public async Task<List<Color>> GetAllColorsAsync() => await _context.Color.ToListAsync();

    public async Task<Color> GetColorByIdAsync(Guid id) => await _context.Color.FirstOrDefaultAsync(c => c.Id == id);

    public async Task<Color> GetDefaultColorAsync() => await _context.Color.FirstOrDefaultAsync(c => c.Name.Contains("Default"));

    public async Task<Color> CreateColorAsync(Color color)
    {
        await _context.Color.AddAsync(color);
        await _context.SaveChangesAsync();
        return color;
    }

    public async Task<Color> UpdateColorAsync(Color color)
    {
        _context.Color.Update(color);
        await _context.SaveChangesAsync();
        return color;
    }

    public async Task DeleteColorAsync(Color color)
    {
        _context.Color.Remove(color);
        await _context.SaveChangesAsync();
    }
}