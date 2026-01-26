namespace Redarbor.TechnicalTest.Domain.ValueObjects;

public record Fax
{
    private const string FAX_REG_FORMAT = @"^\+?[0-9\s\-\(\)]{7,20}$";
    public string Value { get; }
    private Fax(string value)
    {
        Value = value;
    }

    public static Fax Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return new Fax(string.Empty);

        if (!Regex.IsMatch(value, FAX_REG_FORMAT))
        {
            throw new DomainException("Invalid fax format");
        }
        return new Fax(value);
    }

    public static implicit operator string(Fax fax) => fax.Value;
}
