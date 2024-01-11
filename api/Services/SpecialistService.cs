using Pebbles.Models;
using Pebbles.Repositories;

namespace Pebbles.Services;

public class SpecialistService
{
    private readonly ISpecialistRepository _specialistRepository;
    public SpecialistService(ISpecialistRepository specialistRepository)
    {
        _specialistRepository = specialistRepository;
    }

    public async Task<Specialist> GetSpecialistAsync(Guid id) => await _specialistRepository.GetSpecialistByIdAsync(id);

    public async Task<IEnumerable<Specialist>> GetAllSpecialistsAsync() => await _specialistRepository.GetAllSpecialistsAsync();

    public async Task<Specialist> AddSpecialistAsync(Specialist specialist) => await _specialistRepository.CreateSpecialistAsync(specialist);
    public async Task<Specialist> UpdateSpecialistAsync(Specialist specialist) => await _specialistRepository.UpdateSpecialistAsync(specialist);
    public async Task DeleteSpecialistAsync(Specialist specialist) => await _specialistRepository.DeleteSpecialistAsync(specialist.Id);
}