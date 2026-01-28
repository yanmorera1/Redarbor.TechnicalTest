namespace Redarbor.TechnicalTest.Application.Employees.Commands.DeleteEmployee;

internal class DeleteEmployeeHandler
    (IEmployeeRepository employeeRepository)
    : ICommandHandler<DeleteEmployeeCommand, DeleteEmployeeResult>
{
    public async Task<DeleteEmployeeResult> Handle(DeleteEmployeeCommand command, CancellationToken cancellationToken)
    {
        Employee employee = await employeeRepository
            .GetByIdAsync(command.EmployeeId);

        if (employee is null)
        {
            throw new EmployeeNotFoundException(command.EmployeeId);
        }

        await employeeRepository.DeleteAsync(employee, cancellationToken);

        return new DeleteEmployeeResult(IsSuccess: true);
    }
}
