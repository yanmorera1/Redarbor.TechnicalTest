namespace Redarbor.TechnicalTest.Domain.ValueObjects;

public record PortalId(int Value)
{
    public static PortalId Create(int value)
    {
        if (value <= 0) throw new DomainException("The Id should be positive");
        return new PortalId(value);
    }
}
