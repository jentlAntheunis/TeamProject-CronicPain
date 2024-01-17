using Pebbles.Models;
using Pebbles.Context;
using Microsoft.EntityFrameworkCore;

namespace Pebbles.Repositories;

public interface ILoginRepository
{
    Task<Login> CreateLoginAsync(Login login);
    Task<Login> CreateLoginByUserIdAsync(Guid patientId);
    Task<Login> GetLoginsByUserAsync(Guid userId);
}

public class LoginRepository : ILoginRepository
{
    private readonly PebblesContext _context;
    public LoginRepository(PebblesContext context)
    {
        _context = context;
    }

    public async Task<Login> CreateLoginAsync(Login login)
    {
        await _context.Login.AddAsync(login);
        await _context.SaveChangesAsync();
        return login;
    }

    public async Task<Login> CreateLoginByUserIdAsync(Guid userId)
    {
        var login = new Login
        {
            UserId = userId,
            Timestamp = DateTime.Now
        };
        await _context.Login.AddAsync(login);
        await _context.SaveChangesAsync();
        return login;
    }

    public async Task<Login> GetLoginsByUserAsync(Guid userId)
    {
        var login = await _context.Login.FirstOrDefaultAsync(l => l.UserId == userId);
        return login;
    }
}