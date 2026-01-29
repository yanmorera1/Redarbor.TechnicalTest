using Redarbor.TechnicalTest.Application.Employees.Commands.CreateEmployee;

namespace Redarbor.TechnicalTest.Application.Tests.Employees.Commands;

public class CreateEmployeeHandlerTests
{
    private readonly CreateEmployeeHandler _handler;
    private readonly Mock<IEmployeeRepository> _employeeRepository;

    public CreateEmployeeHandlerTests()
    {
        _employeeRepository = new Mock<IEmployeeRepository>();

        _handler = new(_employeeRepository.Object);
    }

    [Fact]
    public async Task Handle_Should_Create_Employee_And_Return_Result()
    {
        // Arrange
        var command = new CreateEmployeeCommand(new CreateEmployeeDto(
            CompanyId: 1,
            Email: "test@mail.com",
            Fax: "123.456.789",
            Name: "Test User",
            Password: "SecurePassword123!",
            PortalId: 2,
            RoleId: 3,
            StatusId: 1,
            Telephone: "987.654.321",
            Username: "testuser"
            ));

        _employeeRepository
            .Setup(m => m.AddAsync(It.IsAny<Employee>(), CancellationToken.None))
            .ReturnsAsync(1)
            .Verifiable();

        // Act
        CreateEmployeeResult result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(1, result.Id);

        _employeeRepository
            .Verify(m => m.AddAsync(It.IsAny<Employee>(), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Handle_Should_Throw_Domain_Exception()
    {
        // Arrange
        var command = new CreateEmployeeCommand(new CreateEmployeeDto(
            CompanyId: 0,
            Email: "test@mail.com",
            Fax: "123.456.789",
            Name: "Test User",
            Password: "SecurePassword123!",
            PortalId: 2,
            RoleId: 3,
            StatusId: 1,
            Telephone: "987.654.321",
            Username: "testuser"
            ));

        // Act & Assert
        var exception = await Assert.ThrowsAnyAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));

        Assert.Equal("Domain Exception \"The Id should be positive\" throws from Domain Layer", exception.Message);
    }

    [Fact]
    public async Task Handle_Should_Throw_Domain_Exception_When_Invalid_Email()
    {
        // Arrange
        var command = new CreateEmployeeCommand(new CreateEmployeeDto(
            CompanyId: 1,
            Email: "testmail.com",
            Fax: "123.456.789",
            Name: "Test User",
            Password: "SecurePassword123!",
            PortalId: 2,
            RoleId: 3,
            StatusId: 1,
            Telephone: "987.654.321",
            Username: "testuser"
            ));

        // Act & Assert
        var exception = await Assert.ThrowsAnyAsync<DomainException>(() => _handler.Handle(command, CancellationToken.None));

        Assert.Equal("Domain Exception \"Invalid email format\" throws from Domain Layer", exception.Message);
    }
}
