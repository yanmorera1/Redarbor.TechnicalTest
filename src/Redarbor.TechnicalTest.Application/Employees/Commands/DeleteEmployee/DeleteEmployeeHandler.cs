using Ardalis.GuardClauses;

namespace Redarbor.TechnicalTest.Application.Employees.Commands.DeleteEmployee;

public class DeleteEmployeeHandler
    (IEmployeeRepository employeeRepository)
    : ICommandHandler<DeleteEmployeeCommand, DeleteEmployeeResult>
{
    public async Task<DeleteEmployeeResult> Handle(DeleteEmployeeCommand command, CancellationToken cancellationToken)
    {
        Guard.Against.Null(command, nameof(command));
        Guard.Against.Null(command.EmployeeId, nameof(command.EmployeeId));
        Guard.Against.NegativeOrZero(command.EmployeeId, nameof(command.EmployeeId));

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
