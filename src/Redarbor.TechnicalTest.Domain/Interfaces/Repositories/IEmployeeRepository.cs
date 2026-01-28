namespace Redarbor.TechnicalTest.Domain.Interfaces.Repositories;

public interface IEmployeeRepository
{
    Task AddAsync(Employee employeem, CancellationToken cancellationToken);
    Task UpdateAsync(Employee employee, CancellationToken cancellationToken);
    Task DeleteAsync(Employee employee, CancellationToken cancellationToken);
    Task<Employee?> GetByIdAsync(int id);
    Task<IEnumerable<Employee>> GetAllAsync();
}
