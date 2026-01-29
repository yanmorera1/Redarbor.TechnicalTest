using Ardalis.GuardClauses;

namespace Redarbor.TechnicalTest.Application.Employees.Commands.CreateEmployee;

public class CreateEmployeeHandler
    (IEmployeeRepository employeeRepository)
    : ICommandHandler<CreateEmployeeCommand, CreateEmployeeResult>
{
    public async Task<CreateEmployeeResult> Handle(CreateEmployeeCommand command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));
        Guard.Against.Null(command.Employee, nameof(command.Employee));

        var employee = CreateEmployee(command.Employee);

        int employeeId = await employeeRepository.AddAsync(employee, cancellationToken);

        return new CreateEmployeeResult(employeeId);
    }

    private Employee CreateEmployee(CreateEmployeeDto employee)
    {
        var newEmployee = Employee.Create(
            CompanyId.Of(employee.CompanyId),
            Email.Create(employee.Email),
            Password.Create(employee.Password),
            PortalId.Of(employee.PortalId),
            RoleId.Of(employee.RoleId),
            employee.StatusId,
            employee.Username,
            employee.Name,
            Fax.Create(employee.Fax),
            Telephone.Create(employee.Telephone)
        );

        return newEmployee;
    }
}
