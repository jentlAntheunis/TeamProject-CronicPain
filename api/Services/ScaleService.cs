using Pebbles.Models;
using Pebbles.Repositories;

namespace Pebbles.Services;

public interface IScaleService
{
    Task<List<Scale>> GetAllScalesAsync();
    Task<Scale> GetScaleByIdAsync(Guid id);
    Task<Guid> GetScaleIdByNameAsync(string name);
    Task<Guid> CreateScaleAsync(Scale scale);
    Task<Scale> UpdateScaleAsync(Scale scale);
    Task DeleteScaleAsync(Scale scale);
}

public class ScaleService : IScaleService
{
    private readonly IScaleRepository _scaleRepository;

    public ScaleService(IScaleRepository scaleRepository)
    {
        _scaleRepository = scaleRepository;
    }

    public async Task<List<Scale>> GetAllScalesAsync() => await _scaleRepository.GetAllScalesAsync();
    public async Task<Scale> GetScaleByIdAsync(Guid id) => await _scaleRepository.GetScaleByIdAsync(id);
    public async Task<Guid> GetScaleIdByNameAsync(string name) => await _scaleRepository.GetScaleIdByNameAsync(name);
    public async Task<Guid> CreateScaleAsync(Scale scale) => await _scaleRepository.CreateScaleAsync(scale);
    public async Task<Scale> UpdateScaleAsync(Scale scale) => await _scaleRepository.UpdateScaleAsync(scale);
    public async Task DeleteScaleAsync(Scale scale) => await _scaleRepository.DeleteScaleAsync(scale);
}