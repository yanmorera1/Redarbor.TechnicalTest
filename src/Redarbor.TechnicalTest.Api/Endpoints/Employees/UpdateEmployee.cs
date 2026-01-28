using Redarbor.TechnicalTest.Application.Employees.Commands.UpdateEmployee;

namespace Redarbor.TechnicalTest.Api.Endpoints.Employees;

public record UpdateEmployeeResponse(bool IsSuccess);

public class UpdateEmployee : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/redarbor", async (ISender sender, UpdateEmployeeDto request) =>
        {
            var command = new UpdateEmployeeCommand(request);
            var result = await sender.Send(command);
            var response = result.Adapt<UpdateEmployeeResponse>();
            return Results.Ok(response);
        })
        .WithName("UpdateEmployee")
        .Produces<UpdateEmployeeResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Updates an employee")
        .WithDescription("Updates an employee");
    }
}
