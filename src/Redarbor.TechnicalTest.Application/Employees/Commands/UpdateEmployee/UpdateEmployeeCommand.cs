namespace Redarbor.TechnicalTest.Application.Employees.Commands.UpdateEmployee;

public record UpdateEmployeeCommand
    (UpdateEmployeeDto Employee)
    : ICommand<UpdateEmployeeResult>;

public record UpdateEmployeeResult(bool IsSuccess);

public class UpdateOrderCommandValidator : AbstractValidator<UpdateEmployeeCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.Employee.Id)
            .NotEmpty().WithMessage("{PropertyName} is required");
    }
}
