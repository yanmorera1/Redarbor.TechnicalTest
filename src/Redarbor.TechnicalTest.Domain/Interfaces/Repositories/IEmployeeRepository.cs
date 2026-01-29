namespace Redarbor.TechnicalTest.Domain.Interfaces.Repositories;

public interface IEmployeeRepository
{
    Task<int> AddAsync(Employee employeem, CancellationToken cancellationToken);
    Task<int> UpdateAsync(Employee employee, CancellationToken cancellationToken);
    Task<int> DeleteAsync(Employee employee, CancellationToken cancellationToken);
    Task<long> LongCountAsync(CancellationToken cancellationToken);
    Task<Employee?> GetByIdAsync(int id);
    Task<IEnumerable<Employee>> GetAllAsync();
    Task<IEnumerable<Employee>> GetAllPaginatedAsync(int pageIndex, int pageSize, string orderBy);
}
