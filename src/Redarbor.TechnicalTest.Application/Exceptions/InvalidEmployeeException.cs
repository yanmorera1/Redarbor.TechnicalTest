using BuildingBlocks.Exceptions;

namespace Redarbor.TechnicalTest.Application.Exceptions;

public class InvalidEmployeeException : BadRequestException
{
    public InvalidEmployeeException(string message) 
        : base($"Invalid employee object: {message}")
    {
        
    }
}
