using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Redarbor.TechnicalTest.Application.Interfaces.Common;
using Redarbor.TechnicalTest.Infrastructure.Common;

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

        ConfigureTypeHandlers();

        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        return services;
    }

    public static void ConfigureTypeHandlers()
    {
        //Ids
        SqlMapper.AddTypeHandler(new StronglyTypedIdTypeHandler<EmployeeId, int>(EmployeeId.Of));
        SqlMapper.AddTypeHandler(new StronglyTypedIdTypeHandler<CompanyId, int>(CompanyId.Of));
        SqlMapper.AddTypeHandler(new StronglyTypedIdTypeHandler<PortalId, int>(PortalId.Of));
        SqlMapper.AddTypeHandler(new StronglyTypedIdTypeHandler<RoleId, int>(RoleId.Of));

        //VOs
        SqlMapper.AddTypeHandler(new StronglyTypedIdTypeHandler<Email, string>(Email.Create));
        SqlMapper.AddTypeHandler(new StronglyTypedIdTypeHandler<Fax, string>(Fax.Create));
        SqlMapper.AddTypeHandler(new StronglyTypedIdTypeHandler<Password, string>(Password.Create));
        SqlMapper.AddTypeHandler(new StronglyTypedIdTypeHandler<Telephone, string>(Telephone.Create));
    }
}
