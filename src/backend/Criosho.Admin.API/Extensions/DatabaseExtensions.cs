using Criosho.Admin.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace Criosho.Admin.API.Extensions;

public static class DatabaseExtensions
{
    public static async Task ApplyDatabaseMigrationsAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<CrioshoAdminDbContext>();
        await dbContext.Database.MigrateAsync();
    }
}
