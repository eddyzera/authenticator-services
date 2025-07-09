using AuthenticatorServices.Domain.Contracts;

namespace AuthenticatorServices.Api.DTOs;

public class AuthenticateUserRequest : IAuthenticateUserServiceRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}