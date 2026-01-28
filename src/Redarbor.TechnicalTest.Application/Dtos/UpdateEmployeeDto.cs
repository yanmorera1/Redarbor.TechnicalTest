using Redarbor.TechnicalTest.Domain.Enums;

namespace Redarbor.TechnicalTest.Application.Dtos;

public record UpdateEmployeeDto(
        int? Id,
        string? Name,
        string Username,
        string Email,
        string? Telephone,
        string? Fax,
        EmployeeStatus StatusId
    );
