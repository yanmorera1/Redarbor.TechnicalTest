namespace Redarbor.TechnicalTest.Application.Employees.Commands.CreateEmployee;

public class CreateEmployeeHandler
    (IEmployeeRepository employeeRepository)
    : ICommandHandler<CreateEmployeeCommand, CreateEmployeeResult>
{
    public async Task<CreateEmployeeResult> Handle(CreateEmployeeCommand command, CancellationToken cancellationToken)
    {
        var employee = CreateEmployee(command.Employee);

        await employeeRepository.AddAsync(employee, cancellationToken);

        return new CreateEmployeeResult(employee.Id.Value);
    }

    private Employee CreateEmployee(EmployeeDto employee)
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
