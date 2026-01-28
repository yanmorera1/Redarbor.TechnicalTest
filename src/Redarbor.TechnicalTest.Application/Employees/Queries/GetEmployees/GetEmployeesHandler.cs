using BuildingBlocks.Pagination;

namespace Redarbor.TechnicalTest.Application.Employees.Queries.GetEmployees;

internal class GetEmployeesHandler
    (IEmployeeRepository employeeRepository)
    : IQueryHandler<GetEmployeesQuery, GetEmployeesResult>
{
    public async Task<GetEmployeesResult> Handle(GetEmployeesQuery query, CancellationToken cancellationToken)
    {
        int pageIndex = query.PaginationRequest.PageIndex;
        int pageSize = query.PaginationRequest.PageSize;

        long totalCount = await employeeRepository.LongCountAsync(cancellationToken);

        var employees = await employeeRepository.GetAllPaginatedAsync(pageIndex, pageSize, "Id");

        var paginatedResult = new PaginatedResult<GetEmployeeDto>(
            pageIndex,
            pageSize,
            totalCount,
            employees.ToDtoList()
            );

        return new GetEmployeesResult(paginatedResult);
    }
}
