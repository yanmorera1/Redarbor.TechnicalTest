namespace Redarbor.TechnicalTest.Domain.ValueObjects;

public record EmployeeId(int Value)
{
    public static EmployeeId Of(int value)
    {
        //if (value <= 0) throw new DomainException("The Id should be positive");
        return new EmployeeId(value);
    }
}
