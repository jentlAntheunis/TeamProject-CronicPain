using Pebbles.Models;
using Pebbles.Repositories;

namespace Pebbles.Services;

public interface ISpecialistRepository
{
    Task<List<Specialist>> GetAllSpecialistsAsync();
    Task<Specialist> GetSpecialistByIdAsync(Guid id);
    Task<Specialist> CreateSpecialistAsync(Specialist specialist);
    Task<Specialist> UpdateSpecialistAsync(Specialist specialist);
    Task DeleteSpecialistAsync(Guid id);
}

public class SpecialistService : ISpecialistService
{
    private readonly ISpecialistRepository _specialistRepository;

    public SpecialistService(ISpecialistRepository specialistRepository)
    {
        _specialistRepository = specialistRepository;
    }

    public async Task<List<Specialist>> GetAllSpecialistsAsync() => await _specialistRepository.GetAllSpecialistsAsync();

    public async Task<Specialist> GetSpecialistByIdAsync(Guid id) => await _specialistRepository.GetSpecialistByIdAsync(id);

    public async Task<Specialist> CreateSpecialistAsync(Specialist specialist) => await _specialistRepository.CreateSpecialistAsync(specialist);

    public async Task<Specialist> UpdateSpecialistAsync(Specialist specialist) => await _specialistRepository.UpdateSpecialistAsync(specialist);

    public async Task DeleteSpecialistAsync(Guid id) => await _specialistRepository.DeleteSpecialistAsync(id);

    
}