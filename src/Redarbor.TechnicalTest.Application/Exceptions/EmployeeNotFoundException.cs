using BuildingBlocks.Exceptions;

namespace Redarbor.TechnicalTest.Application.Exceptions;

public class EmployeeNotFoundException : NotFoundException
{
    public EmployeeNotFoundException(int id)
        : base("Employee", id)
    {
    }
}
