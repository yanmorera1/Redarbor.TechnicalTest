namespace Redarbor.TechnicalTest.Domain.ValueObjects;

public record CompanyId(int Value)
{
    public static CompanyId Create(int value)
    {
        if (value <= 0) throw new DomainException("The Id should be positive");
        return new CompanyId(value);
    }
}
