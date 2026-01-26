namespace Redarbor.TechnicalTest.Domain.ValueObjects;

public record RoleId(int Value)
{
    public static RoleId Create(int value)
    {
        if (value <= 0) throw new DomainException("The Id should be positive");
        return new RoleId(value);
    }
}
