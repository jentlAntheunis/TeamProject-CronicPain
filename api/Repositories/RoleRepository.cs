using Pebbles.Models;
using Pebbles.Context;

using Microsoft.EntityFrameworkCore;

public interface IRoleRepository
{
    Task<List<Role>> GetRolesAsync();
    Task<Role> GetRoleAsync(int id);
    Task<Role> AddRoleAsync(Role role);
    Task<Role> UpdateRoleAsync(Role role);
    Task<Role> DeleteRoleAsync(int id);
}

public class RoleRepository : IRoleRepository
{
    private readonly PebblesContext _context;

    public RoleRepository(PebblesContext context)
    {
        _context = context;
    }

    public async Task<List<Role>> GetRolesAsync() => await _context.Role.ToListAsync();
    public async Task<Role> GetRoleAsync(int id) => await _context.Role.FindAsync(id);
    public async Task<Role> AddRoleAsync(Role role)
    {
        _context.Role.Add(role);
        await _context.SaveChangesAsync();
        return role;
    }

    public async Task<Role> UpdateRoleAsync(Role role)
    {
        _context.Role.Update(role);
        await _context.SaveChangesAsync();
        return role;
    }

    public async Task<Role> DeleteRoleAsync(int id)
    {
        var role = await GetRoleAsync(id);
        _context.Role.Remove(role);
        await _context.SaveChangesAsync();
        return role;
    }    
}