namespace Redarbor.TechnicalTest.Domain.Models;

public class Company : Aggregate<CompanyId>
{
    public string Name { get; private set; } = default!;
    public bool IsActive { get; private set; } = true;
    public static Company Create(CompanyId id, string name, bool isActive)
    {
        ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));

        var company = new Company
        {
            Id = id,
            Name = name,
            IsActive = isActive
        };

        return company;
    }
}
