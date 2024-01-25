using Pebbles.Models;
using Pebbles.Context;

namespace Pebbles.Repositories;

public interface IMovementSessionRepository
{
  Task<MovementSession> GetMovementSessionByIdAsync(Guid id);
  Task<List<MovementSession>> GetAllMovementSessionsAsync();
  Task<List<MovementSession>> GetMovementSessionsByPatientIdAsync(Guid patientId);
  Task<Guid> CreateMovementSessionAsync(MovementSession movementSession);
}

public class MovementSessionRepository : IMovementSessionRepository
{
  private readonly PebblesContext _context;
  public MovementSessionRepository(PebblesContext context)
  {
    _context = context;
  }

  public async Task<MovementSession> GetMovementSessionByIdAsync(Guid id) => await _context.MovementSession.FindAsync(id);
  public async Task<List<MovementSession>> GetAllMovementSessionsAsync() => await Task.FromResult(_context.MovementSession.ToList());
  public async Task<List<MovementSession>> GetMovementSessionsByPatientIdAsync(Guid patientId) => await Task.FromResult(_context.MovementSession.Where(ms => ms.PatientId == patientId).ToList());

  public async Task<Guid> CreateMovementSessionAsync(MovementSession movementSession)
  {
    await _context.MovementSession.AddAsync(movementSession);
    await _context.SaveChangesAsync();
    return movementSession.Id;
  }
}
