using Redarbor.TechnicalTest.Domain.Enums;

namespace Redarbor.TechnicalTest.Application.Dtos;

public record CreateEmployeeDto(
        int CompanyId,
        string Email,
        string? Fax,
        string? Name,
        string Password,
        int PortalId,
        int RoleId,
        int StatusId,
        string? Telephone,
        string Username
    );
