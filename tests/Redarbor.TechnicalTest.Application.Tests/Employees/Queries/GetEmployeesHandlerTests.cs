using Redarbor.TechnicalTest.Application.Employees.Queries.GetEmployees;

namespace Redarbor.TechnicalTest.Application.Tests.Employees.Queries;

public class GetEmployeesHandlerTests
{
    private readonly GetEmployeesHandler _handler;
    private readonly Mock<IEmployeeRepository> _employeeRepository;

    public GetEmployeesHandlerTests()
    {
        _employeeRepository = new Mock<IEmployeeRepository>();

        _handler = new(_employeeRepository.Object);
    }

    [Fact]
    public async Task Handle_Should_Return_Employee_Paginated_List()
    {
        // Arrange
        var query = new GetEmployeesQuery(new PaginationRequest(1, 10));

        var expectedEmployees = new List<Employee>()
        {
            Employee.Create(
                CompanyId.Of(1),
                Email.Create("test@mail.com"),
                Password.Create("1231231"),
                PortalId.Of(1),
                RoleId.Of(1),
                1,
                "testname",
                "Test Name",
                Fax.Create("121.121.121"),
                Telephone.Create("121.121.121")
            ),
        };

        expectedEmployees[0].Id = EmployeeId.Of(1);

        var expectedResult = new GetEmployeesResult(new PaginatedResult<GetEmployeeDto>(
            query.PaginationRequest.PageIndex,
            query.PaginationRequest.PageSize,
            1,
            expectedEmployees.ToDtoList()
            ));

        _employeeRepository
            .Setup(m => m.LongCountAsync(CancellationToken.None))
            .ReturnsAsync(1)
            .Verifiable();

        _employeeRepository
            .Setup(m => m.GetAllPaginatedAsync(query.PaginationRequest.PageIndex, query.PaginationRequest.PageSize, "Id"))
            .ReturnsAsync(expectedEmployees)
            .Verifiable();

        // Act
        GetEmployeesResult result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equivalent(expectedResult, result);

        _employeeRepository
            .Verify(m => m.LongCountAsync(CancellationToken.None), Times.Once);

        _employeeRepository
            .Verify(m => m.GetAllPaginatedAsync(query.PaginationRequest.PageIndex, query.PaginationRequest.PageSize, "Id"), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Throw_NotFoundException()
    {
        // Arrange
        var query = new GetEmployeesQuery(new PaginationRequest(1, 10));

        _employeeRepository
            .Setup(m => m.LongCountAsync(CancellationToken.None))
            .ReturnsAsync(1)
            .Verifiable();

        _employeeRepository
            .Setup(m => m.GetAllPaginatedAsync(query.PaginationRequest.PageIndex, query.PaginationRequest.PageSize, "Id"))
            .ReturnsAsync(It.IsAny<IEnumerable<Employee>>())
            .Verifiable();

        // Act & Assert
        var exception = await Assert
            .ThrowsAnyAsync<NotFoundException>(() => _handler.Handle(query, CancellationToken.None));

        Assert.Equal($"Queried object employees was not found, Key: Employees", exception.Message);

        _employeeRepository
            .Verify(m => m.LongCountAsync(CancellationToken.None), Times.Once);

        _employeeRepository
            .Verify(m => m.GetAllPaginatedAsync(query.PaginationRequest.PageIndex, query.PaginationRequest.PageSize, "Id"), Times.Once);
    }
}
