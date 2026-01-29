using Ardalis.GuardClauses;

namespace Redarbor.TechnicalTest.Application.Employees.Commands.UpdateEmployee;

public class UpdateEmployeeHandler
    (IEmployeeRepository employeeRepository)
    : ICommandHandler<UpdateEmployeeCommand, UpdateEmployeeResult>
{
    public async Task<UpdateEmployeeResult> Handle(UpdateEmployeeCommand command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));
        Guard.Against.Null(command.Employee.Id, nameof(command.Employee.Id));
        Guard.Against.NegativeOrZero(command.Employee.Id, nameof(command.Employee.Id));

        Employee employee = await employeeRepository.GetByIdAsync(command.Employee.Id);
        if (employee is null) {
            throw new EmployeeNotFoundException(command.Employee.Id);
        }

        UpdateEmployeeWithNewValues(employee, command.Employee);

        await employeeRepository.UpdateAsync(employee, cancellationToken);

        return new UpdateEmployeeResult(IsSuccess: true);
    }

    private void UpdateEmployeeWithNewValues(Employee employee, UpdateEmployeeDto employeeDto)
    {
        Email updatedEmail = Email.Create(employeeDto.Email);
        Telephone updatedTelephone = Telephone.Create(employeeDto.Telephone);
        Fax updatedFax = Fax.Create(employeeDto.Fax);

        employee.Update(
            name: employeeDto.Name,
            userName: employeeDto.Username,
            email: updatedEmail,
            telephone: updatedTelephone,
            fax: updatedFax,
            status: employeeDto.StatusId
            );
    }
}
