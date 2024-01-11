using Pebbles.Models;
using Pebbles.Context;
using Microsoft.EntityFrameworkCore;

namespace Pebbles.Repositories;

public interface ISpecialistRepository
{
    Task<List<Specialist>> GetAllSpecialistsAsync();
    Task<Specialist> GetSpecialistByIdAsync(Guid id);
    Task<Specialist> CreateSpecialistAsync(Specialist specialist);
    Task<Specialist> UpdateSpecialistAsync(Specialist specialist);
    Task DeleteSpecialistAsync(Guid id);
}

public class SpecialistRepository : ISpecialistRepository
{
    private readonly PebblesContext _context;

    public SpecialistRepository(PebblesContext context)
    {
        _context = context;
    }

    public async Task<List<Specialist>> GetAllSpecialistsAsync() => await _context.Specialist.ToListAsync();

    public async Task<Specialist> GetSpecialistByIdAsync(Guid id) => await _context.Specialist.FirstOrDefaultAsync(s => s.Id == id);

    public async Task<Specialist> CreateSpecialistAsync(Specialist specialist)
    {
        await _context.Specialist.AddAsync(specialist);
        await _context.SaveChangesAsync();
        return specialist;
    }

    public async Task<Specialist> UpdateSpecialistAsync(Specialist specialist)
    {
        _context.Specialist.Update(specialist);
        await _context.SaveChangesAsync();
        return specialist;
    }

    public async Task DeleteSpecialistAsync(Guid id)
    {
        var specialist = await GetSpecialistByIdAsync(id);
        _context.Specialist.Remove(specialist);
        await _context.SaveChangesAsync();
    }
}