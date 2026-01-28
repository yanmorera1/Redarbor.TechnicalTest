namespace Redarbor.TechnicalTest.Domain.ValueObjects;

public record Fax
{
    private static readonly Regex FaxRegex = new(@"^\d{3}\.\d{3}\.\d{3}$", RegexOptions.Compiled);
    public string Value { get; }
    private Fax(string value)
    {
        Value = value;
    }

    public static Fax Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return new Fax(string.Empty);

        if (!FaxRegex.IsMatch(value))
        {
            throw new DomainException("Invalid fax format");
        }
        return new Fax(value);
    }

    public static implicit operator string(Fax fax) => fax.Value;
}
