namespace Redarbor.TechnicalTest.Domain.ValueObjects;

public record EmployId(int Value)
{
    public static EmployId Create(int value)
    {
        if (value <= 0) throw new DomainException("The Id should be positive");
        return new EmployId(value);
    }
}
