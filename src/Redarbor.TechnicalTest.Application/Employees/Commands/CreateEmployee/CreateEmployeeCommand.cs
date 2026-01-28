namespace Redarbor.TechnicalTest.Application.Employees.Commands.CreateEmployee;

public record CreateEmployeeCommand
    (CreateEmployeeDto Employee)
    : ICommand<CreateEmployeeResult>;

public record CreateEmployeeResult(int Id);

public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
{
    public CreateEmployeeCommandValidator()
    {
        RuleFor(x => x.Employee.Username)
            .MaximumLength(100)
            .NotEmpty().WithMessage("{PropertyName} cannot be empty");

        RuleFor(x => x.Employee.Password)
            .MaximumLength(150)
            .NotEmpty().WithMessage("{PropertyName} cannot be empty");

        RuleFor(x => x.Employee.Email)
            .NotEmpty().WithMessage("{PropertyName} cannot be empty")
            .EmailAddress().WithMessage("{PropertyName} should be an email address");

        RuleFor(x => x.Employee.CompanyId)
            .NotNull().WithMessage("{PropertyName} cannot be null");

        RuleFor(x => x.Employee.PortalId)
            .NotNull().WithMessage("{PropertyName} cannot be null");

        RuleFor(x => x.Employee.RoleId)
            .NotNull().WithMessage("{PropertyName} cannot be null");

        RuleFor(x => x.Employee.StatusId)
            .NotNull().WithMessage("{PropertyName} cannot be null");
    }
}
