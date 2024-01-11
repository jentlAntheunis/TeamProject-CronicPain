using Pebbles.Models;
using Pebbles.Context;
using Microsoft.EntityFrameworkCore;

namespace Pebbles.Repositories;

public interface IUserRepository
{
    Task<List<User>> GetAllUsersAsync();
    Task<User> GetUserByIdAsync(Guid id);
    Task<User> CreateUserAsync(User user);
    Task<User> UpdateUserAsync(User user);
    Task DeleteUserAsync(Guid id);

}

public class UserRepository : IUserRepository
{
    private readonly PebblesContext _context;

    public UserRepository(PebblesContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetAllUsersAsync() => await _context.User.ToListAsync();

    public async Task<User> GetUserByIdAsync(Guid id) => await _context.User.FirstOrDefaultAsync(u => u.Id == id);

    public async Task<User> CreateUserAsync(User user)
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

    public async Task DeleteUserAsync(Guid id)
    {
        var user = await GetUserByIdAsync(id);
        _context.User.Remove(user);
        await _context.SaveChangesAsync();
    }


}