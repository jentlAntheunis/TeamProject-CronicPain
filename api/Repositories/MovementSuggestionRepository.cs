using Pebbles.Models;
using Pebbles.Context;
using Microsoft.EntityFrameworkCore;

namespace Pebbles.Repositories;

public interface IMovementSuggestionRepository
{
    Task<MovementSuggestion> GetMovementSuggestionByIdAsync(Guid id);
    Task<List<MovementSuggestion>> GetMovementSuggestionsByPatientIdAsync(Guid PatientId);
    Task<Guid> CreateMovementSuggestionAsync(MovementSuggestion movementSuggestion);
}

public class MovementSuggestionRepository : IMovementSuggestionRepository
{
    private readonly PebblesContext _context;
    public MovementSuggestionRepository(PebblesContext context)
    {
        _context = context;
    }

    public async Task<MovementSuggestion> GetMovementSuggestionByIdAsync(Guid id) => await _context.MovementSuggestion.FindAsync(id);

    public async Task<List<MovementSuggestion>> GetMovementSuggestionsByPatientIdAsync(Guid PatientId) => await _context.MovementSuggestion.Where(ms => ms.PatientId == PatientId).ToListAsync();

    public async Task<Guid> CreateMovementSuggestionAsync(MovementSuggestion movementSuggestion)
    {
        await _context.MovementSuggestion.AddAsync(movementSuggestion);
        await _context.SaveChangesAsync();
        return movementSuggestion.Id;
    }
}