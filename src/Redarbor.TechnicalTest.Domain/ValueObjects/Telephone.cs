namespace Redarbor.TechnicalTest.Domain.ValueObjects;

public class Telephone
{
    private static readonly Regex TelephoneRegex = new(@"^\d{3}\.\d{3}\.\d{3}$", RegexOptions.Compiled);
    public string Value { get; }
    private Telephone(string value) => Value = value;

    public static Telephone Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return new Telephone(string.Empty);

        if (!TelephoneRegex.IsMatch(value))
        {
            throw new DomainException("Invalid telephone format");
        }

        value = value.SanitizeString();

        return new Telephone(value);
    }

    public static implicit operator string(Telephone telephone) => telephone.Value;
}
