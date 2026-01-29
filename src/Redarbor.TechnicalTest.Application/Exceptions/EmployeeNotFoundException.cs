namespace Redarbor.TechnicalTest.Application.Exceptions;

public class EmployeeNotFoundException : BuildingBlocks.Exceptions.NotFoundException
{
    public EmployeeNotFoundException(int id)
        : base("Employee", id)
    {
    }
}
