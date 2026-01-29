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

        await ValidateEmployee(command.Employee);

        var employee = CreateEmployee(command.Employee);

        EmployeeId employeeId = await employeeRepository.AddAsync(employee, cancellationToken);

        employee.AddDomainEvent(new EmployeeCreatedEvent(employee));

        return new CreateEmployeeResult(employeeId.Value);
    }

    private async Task ValidateEmployee(CreateEmployeeDto employee)
    {
        if (await employeeRepository.IsAnyEmployeeWithEmail(employee.Email))
            throw new InvalidEmployeeException($"Invalid email address");
        if (await employeeRepository.IsAnyEmployeeWithUsename(employee.Username))
            throw new InvalidEmployeeException($"Invalid user name");
        if (!await employeeRepository.CompanyExists(employee.CompanyId))
            throw new InvalidEmployeeException($"Company with id {employee.CompanyId} doesnt exists");
        if (!await employeeRepository.PortalExists(employee.PortalId))
            throw new InvalidEmployeeException($"Portal with id {employee.PortalId} doesnt exists");
        if (!await employeeRepository.RoleExists(employee.RoleId))
            throw new InvalidEmployeeException($"Role with id {employee.RoleId} doesnt exists");
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
