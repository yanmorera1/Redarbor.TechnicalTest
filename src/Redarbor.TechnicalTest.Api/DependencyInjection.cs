using BuildingBlocks.Exceptions.Handler;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using RabbitMQ.Client;

namespace Redarbor.TechnicalTest.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCarter();

        services.AddExceptionHandler<CustomExceptionHandler>();

        services.AddHealthChecks()
            .AddSqlServer(configuration.GetConnectionString("Database")!)
            .AddRabbitMQ(sp =>
                {
                    var config = sp.GetRequiredService<IConfiguration>();

                    string amqpHost = config["MessageBroker:Host"] ?? "localhost";
                    string user = config["MessageBroker:Username"] ?? "guest";
                    string pass = config["MessageBroker:Password"] ?? "guest";

                    string cleanHost = amqpHost.Replace("amqp://", "");
                    string connectionUrl = $"amqp://{user}:{pass}@{cleanHost}/";

                    var factory = new ConnectionFactory
                    {
                        Uri = new Uri(connectionUrl)
                    };

                    return factory.CreateConnectionAsync();
                },
                name: "rabbitmq-check",
                failureStatus: HealthStatus.Unhealthy,
                tags: new[] { "ready" });

        services.AddAuthentication()
            .AddJwtBearer(options =>
            {
                options.Authority = configuration["Authentication:Authority"];
                options.Audience = configuration["Authentication:Audience"];
                options.RequireHttpsMetadata = false;
            });

        services.AddAuthorizationBuilder();

        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.MapCarter();

        app.UseExceptionHandler(options => { });
        app.UseHealthChecks("/health",
            new HealthCheckOptions()
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

        return app;
    }
}
