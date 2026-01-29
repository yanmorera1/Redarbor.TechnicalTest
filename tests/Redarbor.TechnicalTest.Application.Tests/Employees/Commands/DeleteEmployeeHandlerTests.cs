using Redarbor.TechnicalTest.Application.Employees.Commands.DeleteEmployee;

namespace Redarbor.TechnicalTest.Application.Tests.Employees.Commands;

public class DeleteEmployeeHandlerTests
{
    private readonly DeleteEmployeeHandler _handler;
    private readonly Mock<IEmployeeRepository> _employeeRepository;

    public DeleteEmployeeHandlerTests()
    {
        _employeeRepository = new Mock<IEmployeeRepository>();

        _handler = new(_employeeRepository.Object);
    }

    [Fact]
    public async Task Handle_Should_Delete_Employee_And_Return_True()
    {
        // Arrange
        var command = new DeleteEmployeeCommand(1); 
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
        expectedEmployee.Id = EmployeeId.Of(1);

        _employeeRepository
            .Setup(m => m.GetByIdAsync(command.EmployeeId))
            .ReturnsAsync(expectedEmployee)
            .Verifiable();

        _employeeRepository
            .Setup(m => m.DeleteAsync(It.IsAny<Employee>(), CancellationToken.None))
            .ReturnsAsync(1)
            .Verifiable();

        // Act
        DeleteEmployeeResult result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);

        _employeeRepository
            .Verify(m => m.GetByIdAsync(command.EmployeeId), Times.Once);

        _employeeRepository
            .Verify(m => m.DeleteAsync(It.IsAny<Employee>(), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Throw_Domain_Exception()
    {
        // Arrange
        var command = new DeleteEmployeeCommand(0);

        // Act & Assert
        var exception = await Assert.ThrowsAnyAsync<ArgumentException>(() => _handler.Handle(command, CancellationToken.None));

        Assert.Equal($"Required input {nameof(EmployeeId)} cannot be zero or negative. (Parameter '{nameof(EmployeeId)}')", exception.Message);
    }
}
