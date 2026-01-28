namespace Redarbor.TechnicalTest.Application.Interfaces.Data;

public interface IApplicationDbContext
{
    DbSet<Company> Companies {get;}
    DbSet<Employee> Employees {get;}
    DbSet<Portal> Portals {get;}
    DbSet<Role> Roles { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
