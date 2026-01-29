using BuildingBlocks.Pagination;

namespace Redarbor.TechnicalTest.Application.Employees.Queries.GetEmployees;

public class GetEmployeesHandler
    (IEmployeeRepository employeeRepository)
    : IQueryHandler<GetEmployeesQuery, GetEmployeesResult>
{
    public async Task<GetEmployeesResult> Handle(GetEmployeesQuery query, CancellationToken cancellationToken)
    {
        Guard.Against.Null(query, nameof(query));
        Guard.Against.Null(query.PaginationRequest, nameof(query.PaginationRequest));
        Guard.Against.Null(query.PaginationRequest.PageIndex, nameof(query.PaginationRequest.PageIndex));
        Guard.Against.Null(query.PaginationRequest.PageSize, nameof(query.PaginationRequest.PageSize));

        int pageIndex = query.PaginationRequest.PageIndex;
        int pageSize = query.PaginationRequest.PageSize;

        long totalCount = await employeeRepository.LongCountAsync(cancellationToken);

        var employees = await employeeRepository.GetAllPaginatedAsync(pageIndex, pageSize, "Id");

        Guard.Against.NotFound("Employees", employees);
        Guard.Against.Null(employees, nameof(employees));

        var paginatedResult = new PaginatedResult<GetEmployeeDto>(
            pageIndex,
            pageSize,
            totalCount,
            employees.ToDtoList()
            );

        return new GetEmployeesResult(paginatedResult);
    }
}
