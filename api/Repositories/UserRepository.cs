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

    public async Task<List<User>> GetUsersAsync() => await _context.User.ToListAsync();

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
        _context.User.Remove(user);
        await _context.SaveChangesAsync();
    }


}