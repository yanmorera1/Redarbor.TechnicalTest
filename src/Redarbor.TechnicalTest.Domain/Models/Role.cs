namespace Redarbor.TechnicalTest.Domain.Models;

public class Role : Aggregate<RoleId>
{
    public string Name { get; private set; } = default!;
    public bool IsActive { get; private set; } = true;

    public static Role Create(RoleId id, string name, bool isActive)
    {
        ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));

        var role = new Role
        {
            Id = id,
            Name = name,
            IsActive = isActive
        };

        return role;
    }
}
