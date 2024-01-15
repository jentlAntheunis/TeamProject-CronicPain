

namespace Pebbles.Models;

public interface ISoftDelete
{
    public bool IsDeleted { get; set; }
    public DateTimeOffset? DeletedAt { get; set; }

    public void Delete()
    {
        IsDeleted = true;
        DeletedAt = DateTimeOffset.UtcNow;
    }
    public void Undo()
    {
        IsDeleted = false;
        DeletedAt = null;
    }
}