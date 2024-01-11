using Pebbles.Models;
using Pebbles.Context;

using Microsoft.EntityFrameworkCore;

public interface IUserRepository
{
    Task<List<User>> GetUsersAsync();
    Task<User> GetUserAsync(int id);
    Task<User> AddUserAsync(User user);
    Task<User> UpdateUserAsync(User user);
    Task<User> DeleteUserAsync(int id);
}

public class UserRepository : IUserRepository
{
    private readonly PebblesContext _context;

    public UserRepository(PebblesContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetUsersAsync() => await _context.User.ToListAsync();
    public async Task<User> GetUserAsync(int id) => await _context.User.FindAsync(id);
    public async Task<User> AddUserAsync(User user)
    {
        _context.User.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        _context.User.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> DeleteUserAsync(int id)
    {
        var user = await GetUserAsync(id);
        _context.User.Remove(user);
        await _context.SaveChangesAsync();
        return user;
    }    
}