using BuildingBlocks.Pagination;
using Redarbor.TechnicalTest.Application.Employees.Queries.GetEmployeeById;

namespace Redarbor.TechnicalTest.Application.Employees.Queries.GetEmployees;

public record GetEmployeesQuery
    (PaginationRequest PaginationRequest)
    : IQuery<GetEmployeesResult>;

public record GetEmployeesResult(PaginatedResult<GetEmployeeDto> Employees);
