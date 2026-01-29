using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Redarbor.TechnicalTest.Infrastructure.Repositories;

public class EmployeeRepository
    (
        IApplicationDbContext applicationDbContext,
        IDbConnectionFactory dbConnectionFactory
    )
    : IEmployeeRepository
{
    public async Task<int> AddAsync(Employee employee, CancellationToken cancellationToken)
    {
        await applicationDbContext.Employees.AddAsync(employee);
        return await applicationDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> UpdateAsync(Employee employee, CancellationToken cancellationToken)
    {
        applicationDbContext.Employees.Update(employee);
        return await applicationDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> DeleteAsync(Employee employee, CancellationToken cancellationToken)
    {
        applicationDbContext.Employees.Remove(employee);
        return await applicationDbContext.SaveChangesAsync(cancellationToken);
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

    public Task<long> LongCountAsync(CancellationToken cancellationToken)
    {
        return applicationDbContext.Employees.LongCountAsync(cancellationToken);
    }

    public async Task<IEnumerable<Employee>> GetAllPaginatedAsync(int pageIndex, int pageSize, string orderBy)
    {
        using var connection = dbConnectionFactory.CreateConnection();

        const string query =
            """
            SELECT  *
            FROM    ( SELECT    ROW_NUMBER() OVER ( ORDER BY @OrderBy) AS RowNum, *
                      FROM      Employees
                    ) AS result
            WHERE   RowNum >= @PageIndex
                AND RowNum < @PageSize
            ORDER BY RowNum
            """;

        return await connection.QueryAsync<Employee>(query, new {
            PageIndex = pageIndex,
            PageSize = pageSize, 
            OrderBy = orderBy 
        });
    }
}
