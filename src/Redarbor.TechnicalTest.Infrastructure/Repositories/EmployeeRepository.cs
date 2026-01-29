using Redarbor.TechnicalTest.Application.Interfaces.Common;

namespace Redarbor.TechnicalTest.Infrastructure.Repositories;

public class EmployeeRepository
    (
        IApplicationDbContext applicationDbContext,
        IDbConnectionFactory dbConnectionFactory,
        ICurrentUserService currentUserService
    )
    : IEmployeeRepository
{
    public async Task<EmployeeId> AddAsync(Employee employee, CancellationToken cancellationToken)
    {
        await applicationDbContext.Employees.AddAsync(employee);
        await applicationDbContext.SaveChangesAsync(cancellationToken);
        return employee.Id;
    }

    public async Task UpdateAsync(Employee employee, CancellationToken cancellationToken)
    {
        applicationDbContext.Employees.Update(employee);
        await applicationDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Employee employee, CancellationToken cancellationToken)
    {
        employee.DeletedOn = DateTime.UtcNow;
        employee.DeletedBy = currentUserService.GetCurrentUserName();

        applicationDbContext.Employees.Update(employee);
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

    public Task<long> LongCountAsync(CancellationToken cancellationToken)
    {
        return applicationDbContext
            .Employees
            .Where(e => e.DeletedOn == null)
            .LongCountAsync(cancellationToken);
    }

    public async Task<IEnumerable<Employee>> GetAllPaginatedAsync(int pageIndex, int pageSize, string orderBy)
    {
        using var connection = dbConnectionFactory.CreateConnection();

        const string query =
            """
            SELECT *
            FROM (SELECT ROW_NUMBER() OVER ( ORDER BY @OrderBy) AS RowNum, *
                  FROM Employees WHERE DeletedOn IS NULL) AS result
            WHERE RowNum >= @PageIndex
                AND RowNum < @PageSize
            ORDER BY RowNum
            """;

        return await connection.QueryAsync<Employee>(query, new {
            PageIndex = pageIndex,
            PageSize = pageSize, 
            OrderBy = orderBy 
        });
    }

    public async Task<bool> IsAnyEmployeeWithEmail(string email)
    {
        using var connection = dbConnectionFactory.CreateConnection();
        const string sql =
            """
            SELECT * 
            FROM Employees 
            WHERE Email = @Email 
            AND DeletedOn IS NULL
            """;
        var employees = await connection.QueryAsync<Employee>(sql, new { Email = email });
        return employees.Any();
    }

    public async Task<bool> IsAnyEmployeeWithUsename(string UserName)
    {
        using var connection = dbConnectionFactory.CreateConnection();
        const string sql =
            """
            SELECT * 
            FROM Employees 
            WHERE UserName = @UserName 
            AND DeletedOn IS NULL
            """;
        var employees = await connection.QueryAsync<Employee>(sql, new { UserName = UserName });
        return employees.Any();
    }

    public async Task<bool> CompanyExists(int companyId)
    {
        using var connection = dbConnectionFactory.CreateConnection();
        const string sql =
            """
            SELECT * 
            FROM Companies 
            WHERE Id = @Id
            """;
        var companies = await connection.QueryAsync<Company>(sql, new { Id = companyId });
        return companies.Any();
    }

    public async Task<bool> PortalExists(int portalId)
    {
        using var connection = dbConnectionFactory.CreateConnection();
        const string sql =
            """
            SELECT * 
            FROM Portals 
            WHERE Id = @Id
            """;
        var portals = await connection.QueryAsync<Company>(sql, new { Id = portalId });
        return portals.Any();
    }

    public async Task<bool> RoleExists(int roleId)
    {
        using var connection = dbConnectionFactory.CreateConnection();
        const string sql =
            """
            SELECT * 
            FROM Roles 
            WHERE Id = @Id
            """;
        var roles = await connection.QueryAsync<Company>(sql, new { Id = roleId });
        return roles.Any();
    }
}
