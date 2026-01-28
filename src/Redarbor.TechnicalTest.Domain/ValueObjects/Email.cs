namespace Redarbor.TechnicalTest.Domain.ValueObjects;

public record Email
{
    private static readonly Regex EmailRegex = new (@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", RegexOptions.Compiled);
    public string Value { get; }
    private Email(string value)
    {
        Value = value;
    }

    public static Email Create(string value)
    {
        ArgumentNullException.ThrowIfNull(value, nameof(value));
        if (string.IsNullOrWhiteSpace(value) || !EmailRegex.IsMatch(value))
        {
            throw new DomainException("Invalid email format");
        }
        return new Email(value);
    }

    public static implicit operator string(Email email) => email.Value;
}
