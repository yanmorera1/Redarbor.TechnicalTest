namespace Redarbor.TechnicalTest.Application.Employees.Queries.GetEmployeeById;

public record GetEmployeeByIdQuery(int Id)
    : IQuery<GetEmployeeByIdResult>;

public record GetEmployeeByIdResult(GetEmployeeDto Employee);
