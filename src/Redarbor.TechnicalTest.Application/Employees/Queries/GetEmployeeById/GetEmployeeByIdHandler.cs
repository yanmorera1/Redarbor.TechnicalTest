using Ardalis.GuardClauses;

namespace Redarbor.TechnicalTest.Application.Employees.Queries.GetEmployeeById;

public class GetEmployeeByIdHandler
    (IEmployeeRepository employeeRepository)
    : IQueryHandler<GetEmployeeByIdQuery, GetEmployeeByIdResult>
{
    public async Task<GetEmployeeByIdResult> Handle(GetEmployeeByIdQuery query, CancellationToken cancellationToken)
    {
        Guard.Against.Null(query, nameof(query));
        Guard.Against.Null(query.Id, nameof(query.Id));

        Employee employee = await employeeRepository.GetByIdAsync(query.Id);

        if (employee is null)
        {
            throw new EmployeeNotFoundException(query.Id);
        }

        return new GetEmployeeByIdResult(employee.ToDto());
    }
}
