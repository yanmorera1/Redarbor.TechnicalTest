namespace Redarbor.TechnicalTest.Domain.ValueObjects;

public record Fax
{
    private static readonly Regex FaxRegex = new(@"^\+?[0-9\s\-\(\)]{7,20}$", RegexOptions.Compiled);
    public string Value { get; }
    private Fax(string value)
    {
        Value = value;
    }

    public static Fax Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return new Fax(string.Empty);

        value = value.SanitizeString();

        if (!FaxRegex.IsMatch(value))
        {
            throw new DomainException("Invalid fax format");
        }
        return new Fax(value);
    }

    public static implicit operator string(Fax fax) => fax.Value;
}
