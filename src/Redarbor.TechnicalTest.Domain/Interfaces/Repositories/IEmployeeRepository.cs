namespace Redarbor.TechnicalTest.Domain.Interfaces.Repositories;

public interface IEmployeeRepository
{
    Task<EmployeeId> AddAsync(Employee employeem, CancellationToken cancellationToken);
    Task UpdateAsync(Employee employee, CancellationToken cancellationToken);
    Task DeleteAsync(Employee employee, CancellationToken cancellationToken);
    Task<long> LongCountAsync(CancellationToken cancellationToken);
    Task<Employee?> GetByIdAsync(int id);
    Task<IEnumerable<Employee>> GetAllAsync();
    Task<IEnumerable<Employee>> GetAllPaginatedAsync(int pageIndex, int pageSize, string orderBy);
    Task<bool> IsAnyEmployeeWithEmail(string email);
    Task<bool> IsAnyEmployeeWithUsename(string userName);
    Task<bool> CompanyExists(int companyId);
    Task<bool> PortalExists(int portalId);
    Task<bool> RoleExists(int roleId);
}
