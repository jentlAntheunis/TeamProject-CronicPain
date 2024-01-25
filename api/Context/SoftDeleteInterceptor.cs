using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Pebbles.Models;

namespace Pebbles.Context;
public class SoftDeleteInterceptor : SaveChangesInterceptor
{

  //if an element is deleted, it is not actually deleted, but marked as deleted
  public override InterceptionResult<int> SavingChanges(
      DbContextEventData eventData,
      InterceptionResult<int> result)
  {
    if (eventData.Context is null) return result;

    foreach (var entry in eventData.Context.ChangeTracker.Entries())
    {
      if (entry is not { State: EntityState.Deleted, Entity: ISoftDelete delete }) continue;
      entry.State = EntityState.Modified;
      delete.IsDeleted = true;
      delete.DeletedAt = DateTimeOffset.UtcNow;
    }
    return result;
  }
}
