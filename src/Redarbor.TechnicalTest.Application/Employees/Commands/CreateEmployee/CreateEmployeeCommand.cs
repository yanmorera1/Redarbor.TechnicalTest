namespace Redarbor.TechnicalTest.Application.Employees.Commands.CreateEmployee;

public record CreateEmployeeCommand
    (EmployeeDto Employee)
    : ICommand<CreateEmployeeResult>;

public record CreateEmployeeResult(int Id);

public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeCommandValidator()
    {
        RuleFor(x => x.Employee).NotNull();
        RuleFor(x => x.Employee.Name).NotEmpty().MaximumLength(100);
    }
}
