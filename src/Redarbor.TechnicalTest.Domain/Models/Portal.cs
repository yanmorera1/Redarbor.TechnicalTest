namespace Redarbor.TechnicalTest.Domain.Models;

public class Portal : Aggregate<PortalId>
{
    public string Name { get; private set; } = default!;
    public bool IsActive { get; private set; } = true;
    public static Portal Create(PortalId id, string name, bool isActive)
    {
        ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));

        var portal = new Portal
        {
            Id = id,
            Name = name,
            IsActive = isActive
        };

        return portal;
    }
}
