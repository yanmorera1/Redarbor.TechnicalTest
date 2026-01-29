using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Redarbor.TechnicalTest.Application.Interfaces.Common;

namespace Redarbor.TechnicalTest.Infrastructure.Common;

public class CurrentUserService
    (IHttpContextAccessor httpContextAccessor)
    : ICurrentUserService
{
    public string GetCurrentUserName()
    {
        var user = httpContextAccessor.HttpContext?.User;

        if (user?.Identity?.IsAuthenticated != true)
            return "System";

        string[] claimTypes = [ClaimTypes.Email, ClaimTypes.Name, ClaimTypes.NameIdentifier];

        return claimTypes
            .Select(type => user.FindFirst(type)?.Value)
            .FirstOrDefault(value => !string.IsNullOrEmpty(value)) ?? "System";
    }
}
