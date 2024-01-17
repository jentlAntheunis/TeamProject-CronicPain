using Pebbles.Models;
using Pebbles.Context;
using Microsoft.EntityFrameworkCore;

namespace Pebbles.Repositories;

public interface ILoginRepository
{
    Task<Login> CreateLoginAsync(Login login);
    Task<Login> CreateLoginByPatientIdAsync(Guid patientId);
    Task<Login> GetLoginsByPatientAsync(Guid patientId);
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

    public async Task<Login> CreateLoginByPatientIdAsync(Guid patientId)
    {
        var login = new Login
        {
            PatientId = patientId,
            Timestamp = DateTime.Now
        };
        await _context.Login.AddAsync(login);
        await _context.SaveChangesAsync();
        return login;
    }

    public async Task<Login> GetLoginsByPatientAsync(Guid patientId)
    {
        var login = await _context.Login.FirstOrDefaultAsync(l => l.PatientId == patientId);
        return login;
    }
}