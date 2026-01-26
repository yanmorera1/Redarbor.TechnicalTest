using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Redarbor.TechnicalTest.Infrastructure.Persistence.Extensions;

public static class DatabaseExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        context.Database.MigrateAsync().GetAwaiter().GetResult();

        await SeedAsync(context);
    }

    private static async Task SeedAsync(ApplicationDbContext context)
    {
        await SeedCompanyAsync(context);
        await SeedPortalAsync(context);
        await SeedRoleAsync(context);
    }

    private static async Task SeedRoleAsync(ApplicationDbContext context)
    {
        if (!await context.Roles.AnyAsync())
        {
            await context.Roles.AddRangeAsync(InitialData.Roles);
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedPortalAsync(ApplicationDbContext context)
    {
        if (!await context.Portals.AnyAsync())
        {
            await context.Portals.AddRangeAsync(InitialData.Portals);
            await context.SaveChangesAsync();
        }
    }

    private static async Task SeedCompanyAsync(ApplicationDbContext context)
    {
        if (!await context.Companies.AnyAsync())
        {
            await context.Companies.AddRangeAsync(InitialData.Companies);
            await context.SaveChangesAsync();
        }
    }
}
