using Redarbor.TechnicalTest.Application.Employees.Queries.GetEmployeeById;

namespace Redarbor.TechnicalTest.Application.Tests.Employees.Queries;

public class GetEmployeeByIdHandlerTests
{
    private readonly GetEmployeeByIdHandler _handler;
    private readonly Mock<IEmployeeRepository> _employeeRepository;

    public GetEmployeeByIdHandlerTests()
    {
        _employeeRepository = new Mock<IEmployeeRepository>();

        _handler = new(_employeeRepository.Object);
    }

    [Fact]
    public async Task Handle_Should_Return_Employee_Paginated_List()
    {
        // Arrange
        var query = new GetEmployeeByIdQuery(1);

        var expectedEmployee = Employee.Create(
                CompanyId.Of(1),
                Email.Create("test@mail.com"),
                Password.Create("1231231"),
                PortalId.Of(1),
                RoleId.Of(1),
                1,
                "testname",
                "Test Name",
                Fax.Create("121.121.121"),
                Telephone.Create("121.121.121"));

        expectedEmployee.Id = EmployeeId.Of(1);

        var expectedResult = new GetEmployeeByIdResult(expectedEmployee.ToDto());

        _employeeRepository
            .Setup(m => m.GetByIdAsync(query.Id))
            .ReturnsAsync(expectedEmployee)
            .Verifiable();

        // Act
        GetEmployeeByIdResult result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equivalent(expectedResult, result);

        _employeeRepository
            .Verify(m => m.GetByIdAsync(query.Id), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Throw_EmployeeNotFoundException()
    {
        // Arrange
        var query = new GetEmployeeByIdQuery(1);

        _employeeRepository
            .Setup(m => m.GetByIdAsync(query.Id))
            .ReturnsAsync(It.IsAny<Employee>())
            .Verifiable();

        // Act & Assert
        var exception = await Assert
            .ThrowsAnyAsync<EmployeeNotFoundException>(() => _handler.Handle(query, CancellationToken.None));

        Assert.Equal($"Entity \"{nameof(Employee)}\" ({query.Id}) was not found", exception.Message);

        _employeeRepository
            .Verify(m => m.GetByIdAsync(query.Id), Times.Once);
    }
}
