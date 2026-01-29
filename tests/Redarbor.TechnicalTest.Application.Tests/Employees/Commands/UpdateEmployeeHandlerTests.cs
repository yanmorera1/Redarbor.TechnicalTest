using Redarbor.TechnicalTest.Application.Employees.Commands.UpdateEmployee;

namespace Redarbor.TechnicalTest.Application.Tests.Employees.Commands;

public class UpdateEmployeeHandlerTests
{
    private readonly UpdateEmployeeHandler _handler;
    private readonly Mock<IEmployeeRepository> _employeeRepository;

    public UpdateEmployeeHandlerTests()
    {
        _employeeRepository = new Mock<IEmployeeRepository>();

        _handler = new(_employeeRepository.Object);
    }

    [Fact]
    public async Task Handle_Should_Update_Employee_And_Return_True()
    {
        // Arrange
        var command = new UpdateEmployeeCommand(new UpdateEmployeeDto(
            Id: 1,
            Email: "test_updated@mail.com",
            Fax: "123.456.789",
            Name: null,
            StatusId: Domain.Enums.EmployeeStatus.Suspended,
            Telephone: "987.654.321",
            Username: "testuser"
            ));

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
            Telephone.Create("121.121.121")
        );

        _employeeRepository
            .Setup(m => m.GetByIdAsync(command.Employee.Id))
            .ReturnsAsync(expectedEmployee)
            .Verifiable();

        _employeeRepository
            .Setup(m => m.UpdateAsync(It.IsAny<Employee>(), CancellationToken.None))
            .Verifiable();

        // Act
        UpdateEmployeeResult result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);

        _employeeRepository
            .Verify(m => m.GetByIdAsync(command.Employee.Id), Times.Once);

        _employeeRepository
            .Verify(m => m.UpdateAsync(It.IsAny<Employee>(), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Throw_EmployeeNotFoundException()
    {
        // Arrange
        var command = new UpdateEmployeeCommand(new UpdateEmployeeDto(
            Id: 1,
            Email: "test_updated@mail.com",
            Fax: "123.456.789",
            Name: null,
            StatusId: Domain.Enums.EmployeeStatus.Suspended,
            Telephone: "987.654.321",
            Username: "testuser"
            ));

        _employeeRepository
            .Setup(m => m.GetByIdAsync(command.Employee.Id))
            .Verifiable();

        // Act & Assert
        var exception = await Assert
            .ThrowsAnyAsync<EmployeeNotFoundException>(() => _handler.Handle(command, CancellationToken.None));

        Assert.Equal($"Entity \"Employee\" ({command.Employee.Id}) was not found", exception.Message);

        _employeeRepository
            .Verify(m => m.GetByIdAsync(command.Employee.Id), Times.Once);
    }
}
