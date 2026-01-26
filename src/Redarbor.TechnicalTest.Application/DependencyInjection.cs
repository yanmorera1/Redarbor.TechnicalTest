using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Redarbor.TechnicalTest.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddAplication(
        this IServiceCollection services,
        IConfiguration configuration
        )
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(assembly);
        });

        return services;
    }
}
