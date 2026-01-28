namespace Redarbor.TechnicalTest.Infrastructure.Repositories;

public class EmployeeRepository
    (
        IApplicationDbContext applicationDbContext,
        IDbConnectionFactory dbConnectionFactory
    )
    : IEmployeeRepository
{
    public async Task AddAsync(Employee employee, CancellationToken cancellationToken)
    {
        await applicationDbContext.Employees.AddAsync(employee);
        await applicationDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Employee employee, CancellationToken cancellationToken)
    {
        applicationDbContext.Employees.Update(employee);
        await applicationDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Employee employee, CancellationToken cancellationToken)
    {
        applicationDbContext.Employees.Remove(employee);
        await applicationDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        using var connection = dbConnectionFactory.CreateConnection();
        const string sql =
            """
            SELECT * 
            FROM Employees 
            AND DeletedOn IS NULL
            """;
        return await connection.QueryAsync<Employee>(sql);


    }

    public async Task<Employee?> GetByIdAsync(int id)
    {
        using var connection = dbConnectionFactory.CreateConnection();
        const string sql =
            """
            SELECT * 
            FROM Employees 
            WHERE Id = @Id 
            AND DeletedOn IS NULL
            """;
        return await connection.QueryFirstOrDefaultAsync<Employee>(sql, new { Id = id });
    }
}
