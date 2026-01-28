namespace Redarbor.TechnicalTest.Infrastructure.Persistence.Extensions;

internal class InitialData
{
    public static IEnumerable<Role> Roles =>
    new List<Role>
    {
        Role.Create(RoleId.Of(1), "Employee", true),
        Role.Create(RoleId.Of(2), "Admin", true)
    };
    public static IEnumerable<Portal> Portals =>
    new List<Portal>
    {
        Portal.Create(PortalId.Of(1), "Web", true),
        Portal.Create(PortalId.Of(2), "Mobile", true)
    };
    public static IEnumerable<Company> Companies =>
    new List<Company>
    {
        Company.Create(CompanyId.Of(1), "Computrabajo", true),
        Company.Create(CompanyId.Of(2), "Infojobs", true)
    };
}
