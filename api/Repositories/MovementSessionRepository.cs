using Pebbles.Models;
using Pebbles.Context;

namespace Pebbles.Repositories;

public interface IMovementSessionRepository
{
    Task<MovementSession> GetMovementSessionByIdAsync(Guid id);
    Task<IEnumerable<MovementSession>> GetAllMovementSessionsAsync();
    Task<Guid> CreateMovementSessionAsync(MovementSession movementSession);
    Task<MovementSession> UpdateMovementSessionAsync(MovementSession movementSession);
}

public class MovementSessionRepository : IMovementSessionRepository
{
    private readonly PebblesContext _context;
    public MovementSessionRepository(PebblesContext context)
    {
        _context = context;
    }

    public async Task<MovementSession> GetMovementSessionByIdAsync(Guid id) => await _context.MovementSession.FindAsync(id);

    public async Task<IEnumerable<MovementSession>> GetAllMovementSessionsAsync() => await Task.FromResult(_context.MovementSession.ToList());

    public async Task<Guid> CreateMovementSessionAsync(MovementSession movementSession)
    {
        await _context.MovementSession.AddAsync(movementSession);
        await _context.SaveChangesAsync();
        return movementSession.Id;
    }

    public async Task<MovementSession> UpdateMovementSessionAsync(MovementSession movementSession)
    {
        _context.MovementSession.Update(movementSession);
        await _context.SaveChangesAsync();
        return movementSession;
    }
}