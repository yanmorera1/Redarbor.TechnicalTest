namespace Redarbor.TechnicalTest.Domain.ValueObjects;

public record Email
{
    private const string EMAIL_REGEX_FORMAT = @"/^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/i";
    public string Value { get; }
    private Email(string value)
    {
        Value = value;
    }

    public static Email Create(string value)
    {
        ArgumentNullException.ThrowIfNull(value, nameof(value));
        if (string.IsNullOrWhiteSpace(value) || !Regex.IsMatch(value, EMAIL_REGEX_FORMAT))
        {
            throw new DomainException("Invalid email format");
        }
        return new Email(value);
    }

    public static implicit operator string(Email email) => email.Value;
}
