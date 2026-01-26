namespace Redarbor.TechnicalTest.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options
    )
    {
    }

    public DbSet<Company> Companies => Set<Company>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Portal> Portals => Set<Portal>();
    public DbSet<Role> Roles => Set<Role>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ApplicationDbContext).Assembly
        );
        base.OnModelCreating(modelBuilder);
    }
}
