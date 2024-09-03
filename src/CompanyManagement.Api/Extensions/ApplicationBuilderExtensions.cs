using CompanyManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CompanyManagement.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static async Task ApplyMigratinosAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        dbContext.Database.EnsureDeleted();

        await dbContext.Database.MigrateAsync();
    }
}
