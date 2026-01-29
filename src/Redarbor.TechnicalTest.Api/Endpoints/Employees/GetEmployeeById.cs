using Redarbor.TechnicalTest.Application.Employees.Queries.GetEmployeeById;

namespace Redarbor.TechnicalTest.Api.Endpoints.Employees;

public record GetEmployeeByIdResponse(GetEmployeeDto Employee);

public class GetEmployeeById : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/redarbor/{id}", async (ISender sender, int id) =>
        {
            var query = new GetEmployeeByIdQuery(id);
            var result = await sender.Send(query);
            var response = result.Adapt<GetEmployeeByIdResponse>();
            return Results.Ok(response);
        })
        .WithName("GetEmployeeById")
        .Produces<GetEmployeeByIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status401Unauthorized)
        .WithSummary("Get an employee by id")
        .WithDescription("Get an employee by id")
        .RequireAuthorization();
    }
}
