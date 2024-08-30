using CompanyManagement.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CompanyManagement.Infrastructure.Interceptors;

internal class SoftDeleteInterceptor : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        if (eventData is null)
        {
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        var entries = eventData.Context!.ChangeTracker.Entries<ISoftDeletable>()
            .Where(e => e.State == EntityState.Deleted);

        if (!entries.Any())
        {
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        foreach (var entry in entries)
        {
            entry.State = EntityState.Modified;

            entry.Entity.Delete();
        }

        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
