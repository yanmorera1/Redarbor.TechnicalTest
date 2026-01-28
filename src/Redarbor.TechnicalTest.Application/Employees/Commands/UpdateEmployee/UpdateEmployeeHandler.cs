namespace Redarbor.TechnicalTest.Application.Employees.Commands.UpdateEmployee;

internal class UpdateEmployeeHandler
    (IEmployeeRepository employeeRepository)
    : ICommandHandler<UpdateEmployeeCommand, UpdateEmployeeResult>
{
    public async Task<UpdateEmployeeResult> Handle(UpdateEmployeeCommand command, CancellationToken cancellationToken)
    {
        Employee employee = await employeeRepository.GetByIdAsync(command.Employee.Id.Value);
        if (employee is null) {
            throw new EmployeeNotFoundException(command.Employee.Id.Value);
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
