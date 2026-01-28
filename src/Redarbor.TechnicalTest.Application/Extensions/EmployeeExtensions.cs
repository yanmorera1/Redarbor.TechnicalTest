namespace Redarbor.TechnicalTest.Application.Extensions;

public static class EmployeeExtensions
{
    public static IEnumerable<GetEmployeeDto> ToDtoList(this IEnumerable<Employee> employees)
        => employees.Select(e => e.ToDto());

    public static GetEmployeeDto ToDto(this Employee employee)
        => new GetEmployeeDto(
            employee.Id.Value,
            employee.CompanyId.Value,
            employee.Email.Value,
            employee.Fax.Value,
            employee.Name,
            employee.PortalId.Value,
            employee.RoleId.Value,
            employee.Status,
            employee.Telephone.Value,
            employee.Username
        );
}
