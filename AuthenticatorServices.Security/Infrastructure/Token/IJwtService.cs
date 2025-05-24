using System.Security.Claims;

namespace AuthenticatorServices.Security.Infrastructure.Token
{
    public interface IJwtService
    {
        string GenerateToken(string userId, string email, IEnumerable<string> roles);
        ClaimsPrincipal? ValidateToken(string token);
    }
} 