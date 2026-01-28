using BuildingBlocks.Pagination;

namespace Redarbor.TechnicalTest.Application.Employees.Queries.GetEmployees;

public record GetEmployeesQuery
    (PaginationRequest PaginationRequest)
    : IQuery<GetEmployeesResult>;

public record GetEmployeesResult(PaginatedResult<GetEmployeeDto> Employees);
