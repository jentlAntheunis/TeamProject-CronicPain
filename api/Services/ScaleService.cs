using Pebbles.Models;
using Pebbles.Repositories;

namespace Pebbles.Services;

public interface IScaleService
{
    Task<Scale> AddScaleAsync(Scale scale);
    Task<Scale> GetScaleAsync(Guid id);
    Task<Scale> GetScaleByNameAsync(string name);
    Task<List<Scale>> GetScalesAsync();
    Task<Scale> UpdateScaleAsync(Scale scale);
    Task DeleteScaleAsync(Guid id);
}

public class ScaleService : IScaleService
{
    private readonly IScaleRepository _scaleRepository;

    public ScaleService(IScaleRepository scaleRepository)
    {
        _scaleRepository = scaleRepository;
    }

    public async Task<Scale> AddScaleAsync(Scale scale) => await _scaleRepository.AddScaleAsync(scale);

    public async Task<Scale> GetScaleAsync(Guid id) => await _scaleRepository.GetScaleByIdAsync(id);

    public async Task<List<Scale>> GetScalesAsync() => await _scaleRepository.GetAllScalesAsync();

    public async Task<Scale> GetScaleByNameAsync(string name) => await _scaleRepository.GetScaleByNameAsync(name);

    public async Task<Scale> UpdateScaleAsync(Scale scale) => await _scaleRepository.UpdateScaleAsync(scale);

    public async Task DeleteScaleAsync(Guid id)
    {
        var scale = await _scaleRepository.GetScaleByIdAsync(id);
        await _scaleRepository.DeleteScaleAsync(scale);
    }
}