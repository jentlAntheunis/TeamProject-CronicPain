using Pebbles.Models;
using Pebbles.Context;
using Microsoft.EntityFrameworkCore;

namespace Pebbles.Repositories;

public interface IAvatarRepository
{
    Task<Avatar> GetAvatarByIdAsync(Guid id);
    Task<Avatar> AddAvatarAsync(Avatar avatar);
    Task<Avatar> UpdateAvatarAsync(Avatar avatar);
    Task DeleteAvatarAsync(Avatar avatar);
}

public class AvatarRepository : IAvatarRepository
{
    private readonly PebblesContext _context;

    public AvatarRepository(PebblesContext context)
    {
        _context = context;
    }

    public async Task<Avatar> GetAvatarByIdAsync(Guid id) => await _context.Avatar.Where(a => a.IsDeleted == false).FirstOrDefaultAsync(a => a.Id == id);

    public async Task<Avatar> AddAvatarAsync(Avatar avatar)
    {
        await _context.Avatar.AddAsync(avatar);
        await _context.SaveChangesAsync();
        return avatar;
    }

    public async Task<Avatar> UpdateAvatarAsync(Avatar avatar)
    {
        _context.Avatar.Update(avatar);
        await _context.SaveChangesAsync();
        return avatar;
    }

    public async Task DeleteAvatarAsync(Avatar avatar)
    {
        _context.Avatar.Remove(avatar);
        await _context.SaveChangesAsync();
    }
}