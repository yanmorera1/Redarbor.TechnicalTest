using Redarbor.TechnicalTest.Domain.Enums;

namespace Redarbor.TechnicalTest.Application.Dtos;

public record GetEmployeeDto(
        int? Id,
        int CompanyId,
        string Email,
        string? Fax,
        string? Name,
        int PortalId,
        int RoleId,
        EmployeeStatus StatusId,
        string? Telephone,
        string Username
    );
