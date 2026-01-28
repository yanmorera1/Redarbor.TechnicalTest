using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Redarbor.TechnicalTest.Infrastructure.Persistence;
using Redarbor.TechnicalTest.Infrastructure.Persistence.Factories;
using Redarbor.TechnicalTest.Infrastructure.Persistence.Interceptors;
using Redarbor.TechnicalTest.Infrastructure.Repositories;

namespace Redarbor.TechnicalTest.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("Database");

        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, options) => {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseSqlServer(connectionString, options => options.EnableRetryOnFailure());
        });

        services.AddSingleton<IDbConnectionFactory>(_ => new DbConnectionFactory(connectionString!));

        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        return services;
    }
}
