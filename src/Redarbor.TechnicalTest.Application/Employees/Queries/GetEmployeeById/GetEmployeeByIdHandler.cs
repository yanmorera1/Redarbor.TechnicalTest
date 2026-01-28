namespace Redarbor.TechnicalTest.Application.Employees.Queries.GetEmployeeById;

internal class GetEmployeeByIdHandler
    (IEmployeeRepository employeeRepository)
    : IQueryHandler<GetEmployeeByIdQuery, GetEmployeeByIdResult>
{
    public async Task<GetEmployeeByIdResult> Handle(GetEmployeeByIdQuery query, CancellationToken cancellationToken)
    {
        Employee employee = await employeeRepository.GetByIdAsync(query.Id);

        if (employee is null)
        {
            throw new EmployeeNotFoundException(query.Id);
        }

        return new GetEmployeeByIdResult(employee.ToDto());
    }
}
