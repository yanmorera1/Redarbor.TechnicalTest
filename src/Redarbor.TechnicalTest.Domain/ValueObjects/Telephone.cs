namespace Redarbor.TechnicalTest.Domain.ValueObjects;

public class Telephone
{
    private const string TELEPHONE_REGEX_FORMAT = @"^\d{3}\.\d{3}\.\d{3}$";
    public string Value { get; }
    private Telephone(string value) => Value = value;

    public static Telephone Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return new Telephone(string.Empty);

        if (!Regex.IsMatch(value, TELEPHONE_REGEX_FORMAT))
        {
            throw new DomainException("Invalid telephone format");
        }
        return new Telephone(value);
    }

    public static implicit operator string(Telephone telephone) => telephone.Value;
}
