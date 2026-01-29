using System.Runtime.CompilerServices;

namespace Redarbor.TechnicalTest.Domain.Models;

public class Employee : Aggregate<EmployeeId>
{
    public CompanyId CompanyId { get; private set; } = default!;
    public Email Email { get; private set; } = default!;
    public Fax Fax { get; private set; } = default!;
    public string? Name { get; private set; } = default!;
    public DateTime? LastLogin { get; private set; }
    public Password Password { get; private set; } = default!;
    public PortalId PortalId { get; private set; } = default!;
    public RoleId RoleId { get; private set; } = default!;
    public int StatusId { get; private set; }
    public EmployeeStatus Status
    {
        get => (EmployeeStatus)StatusId;
        private set => StatusId = (int)value;
    }
    public Telephone Telephone { get; private set; } = default!;
    public string Username { get; private set; } = default!;

    public static Employee Create(
        CompanyId companyId,
        Email email,
        Password password,
        PortalId portalId,
        RoleId roleId,
        int statusId,
        string userName,
        string name = "",
        Fax fax = null!,
        Telephone telephone = null!,
        DateTime? lastLogin = null!
        )
    {
        if (!Enum.IsDefined(typeof(EmployeeStatus), statusId))
        {
            throw new DomainException("Invalid StatusId");
        }

        Employee employee = new()
        {
            CompanyId = companyId,
            Email = email,
            Password = password,
            PortalId = portalId,
            RoleId = roleId,
            StatusId = statusId,
            Username = userName,
            Name = name,
            Fax = fax,
            Telephone = telephone,
            LastLogin = lastLogin
        };

        employee.AddDomainEvent(new EmployeeCreatedEvent(employee));

        return employee;
    }

    public void Update(
    string? name,
    string? userName,
    Email? email,
    Telephone? telephone,
    Fax? fax,
    EmployeeStatus? status)
    {
        bool hasChanged = false;

        if (!string.IsNullOrWhiteSpace(name) && Name != name)
        {
            Name = name;
            hasChanged = true;
        }

        if (!string.IsNullOrWhiteSpace(userName) && Username != userName)
        {
            Username = userName;
            hasChanged = true;
        }

        if (email is not null && Email != email)
        {
            Email = email;
            hasChanged = true;
        }

        if (telephone is not null && !string.IsNullOrEmpty(telephone.Value) && Telephone != telephone)
        {
            Telephone = telephone;
            hasChanged = true;
        }

        if (fax is not null && !string.IsNullOrEmpty(fax.Value) && Fax != fax)
        {
            Fax = fax;
            hasChanged = true;
        }

        if (status.HasValue && Status != status.Value)
        {
            Status = status.Value;
            hasChanged = true;
        }

        if (hasChanged)
        {
            AddDomainEvent(new EmployeeUpdatedEvent(this));
        }
    }
}
