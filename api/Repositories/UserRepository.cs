using Pebbles.Models;
using Pebbles.Context;
using Microsoft.EntityFrameworkCore;

namespace Pebbles.Repositories;

public interface IUserRepository
{
    Task<List<User>> GetUsersAsync();
    Task<User> GetUserByIdAsync(Guid id);
    Task<User> AddUserAsync(User user);
    Task<User> UpdateUserAsync(User user);
    Task DeleteUserAsync(User user);

}

public class UserRepository : IUserRepository
{
    private readonly PebblesContext _context;

    public UserRepository(IConfiguration configuration)
    {
        _context = new PebblesContext(configuration);
    }

    public async Task<List<User>> GetUsersAsync() => await _context.User.Where(u => u.IsDeleted == false).ToListAsync();

    public async Task<User> GetUserByIdAsync(Guid id) => await _context.User.FirstOrDefaultAsync(u => u.Id == id);

    public async Task<User> AddUserAsync(User user)
    {
        await _context.User.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        _context.User.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task DeleteUserAsync(User user)
    {
        //check if user is a patient
        var patient = await _context.Patient.FirstOrDefaultAsync(p => p.Id == user.Id);
        if(patient != null)
        {
            //check if patient has an avatar
            var avatar = await _context.Avatar.FirstOrDefaultAsync(a => a.Id == patient.AvatarId);
            if(avatar != null)
            {
                //delete avatar
                _context.Avatar.Remove(avatar);
            }
            //delete patient
            _context.Patient.Remove(patient);
        }
        //check if user is a specialist
        var specialist = await _context.Specialist.FirstOrDefaultAsync(s => s.Id == user.Id);
        if(specialist != null)
        {
            //delete specialist
            _context.Specialist.Remove(specialist);
        }
        _context.SaveChanges();
    }
}