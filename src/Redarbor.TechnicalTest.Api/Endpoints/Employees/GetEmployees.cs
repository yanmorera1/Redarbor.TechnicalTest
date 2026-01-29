using BuildingBlocks.Pagination;
using Redarbor.TechnicalTest.Application.Employees.Queries.GetEmployees;

namespace Redarbor.TechnicalTest.Api.Endpoints.Employees;

public record GetEmployeesResponse(PaginatedResult<GetEmployeeDto> Employees);

public class GetEmployees : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/redarbor", async (ISender sender, [AsParameters] PaginationRequest request) =>
        {
            var query = new GetEmployeesQuery(request);
            var result = await sender.Send(query);
            var response = result.Adapt<GetEmployeesResponse>();
            return Results.Ok(response);
        })
        .WithName("GetEmployees")
        .Produces<GetEmployeeByIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status401Unauthorized)
        .WithSummary("Get all the employees")
        .WithDescription("Get all the employees")
        .RequireAuthorization();
    }
}
