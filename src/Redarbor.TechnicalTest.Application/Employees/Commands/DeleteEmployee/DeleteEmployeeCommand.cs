namespace Redarbor.TechnicalTest.Application.Employees.Commands.DeleteEmployee;

public record DeleteEmployeeCommand
    (int EmployeeId)
    : ICommand<DeleteEmployeeResult>;

public record DeleteEmployeeResult(bool IsSuccess);

public class DeleteEmployeeCommandValidator : AbstractValidator<DeleteEmployeeCommand>
{
    public DeleteEmployeeCommandValidator()
    {
        RuleFor(x => x.EmployeeId)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than zero.");
    }
}
