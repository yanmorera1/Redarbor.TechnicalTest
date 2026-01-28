namespace Redarbor.TechnicalTest.Application.Dtos;

public record EmployeeDto(
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
