using Redarbor.TechnicalTest.Application.Employees.Commands.DeleteEmployee;

namespace Redarbor.TechnicalTest.Api.Endpoints.Employees;

public record DeleteEmployeeResponse(bool IsSuccess);
public class DeleteEmployee : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/redarbor/{id}", async (ISender sender, int id) =>
        {
            var command = new DeleteEmployeeCommand(id);
            var result = await sender.Send(command);
            var response = result.Adapt<DeleteEmployeeResponse>();
            return Results.Ok(response);
        })
        .WithName("DeleteEmployee")
        .Produces<DeleteEmployeeResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status401Unauthorized)
        .WithSummary("Deletes an employee")
        .WithDescription("Deletes an employee")
        .RequireAuthorization();
    }
}
