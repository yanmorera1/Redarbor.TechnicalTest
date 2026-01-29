namespace Redarbor.TechnicalTest.Api.Endpoints.Employees;

public record CreateEmployeeResponse(int Id);

public class CreateEmployee : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/redarbor", async (ISender sender, CreateEmployeeDto request) =>
        {
            var command = new CreateEmployeeCommand(request);
            var result = await sender.Send(command);
            var response = result.Adapt<CreateEmployeeResponse>();
            return Results.Created($"/redarbor/{response.Id}", response);
        })
        .WithName("CreateEmployee")
        .Produces<CreateEmployeeResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status401Unauthorized)
        .WithSummary("Creates an employee")
        .WithDescription("Creates an employee")
        .RequireAuthorization();
    }
}
