using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Pebbles.Models;

namespace Pebbles.Context;

public class SoftDeleteInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        if (eventData.Context is null) return result;

        foreach (var entry in eventData.Context.ChangeTracker.Entries())
        {
            if (entry.Entity is not { } || entry.State != EntityState.Deleted) continue;

            if (entry.Entity is ISoftDelete deleteEntity)
            {
                // Soft delete logic
                entry.State = EntityState.Modified;
                deleteEntity.IsDeleted = true;
                deleteEntity.DeletedAt = DateTimeOffset.UtcNow;
            }
        }

        return base.SavingChanges(eventData, result);
    }
}
