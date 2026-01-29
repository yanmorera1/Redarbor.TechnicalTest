using Redarbor.TechnicalTest.Application.Employees.Commands.UpdateEmployee;

namespace Redarbor.TechnicalTest.Application.Employees.Queries.GetEmployeeById;

public record GetEmployeeByIdQuery(int Id)
    : IQuery<GetEmployeeByIdResult>;

public record GetEmployeeByIdResult(GetEmployeeDto Employee);

public class GetEmployeeByIdQueryValidator : AbstractValidator<GetEmployeeByIdQuery>
{
    public GetEmployeeByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("{PropertyName} is required");
    }
}
