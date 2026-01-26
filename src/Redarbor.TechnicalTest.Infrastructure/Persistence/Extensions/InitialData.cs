namespace Redarbor.TechnicalTest.Infrastructure.Persistence.Extensions;

internal class InitialData
{
    public static IEnumerable<Role> Roles =>
    new List<Role>
    {
        Role.Create(RoleId.Create(1), "Employee", true),
        Role.Create(RoleId.Create(2), "Admin", true)
    };
    public static IEnumerable<Portal> Portals =>
    new List<Portal>
    {
        Portal.Create(PortalId.Create(1), "Web", true),
        Portal.Create(PortalId.Create(2), "Mobile", true)
    };
    public static IEnumerable<Company> Companies =>
    new List<Company>
    {
        Company.Create(CompanyId.Create(1), "Computrabajo", true),
        Company.Create(CompanyId.Create(2), "Infojobs", true)
    };
}
