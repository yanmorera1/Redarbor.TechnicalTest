namespace Redarbor.TechnicalTest.Domain.ValueObjects;

public record Password
{
    public string Value { get; }
    private Password(string value)
    {
        Value = value;
    }

    public static Password Create(string value)
    {
        ArgumentNullException.ThrowIfNull(value, nameof(value));
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new DomainException("Invalid email format");
        }
        if (value.Length < 4)
        {
            throw new DomainException("Password too short");
        }
        return new Password(value);
    }
}
