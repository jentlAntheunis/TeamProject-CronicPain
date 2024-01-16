using Pebbles.Models;
using Pebbles.Context;
using Microsoft.EntityFrameworkCore;

namespace Pebbles.Repositories;

public interface IColorRepository
{
    Task<List<Color>> GetAllColorsAsync();
    Task<Color> GetColorByIdAsync(Guid id);
    Task<Color> CreateColorAsync(Color color);
    Task<Color> UpdateColorAsync(Color color);
    Task DeleteColorAsync(Color color);
}

