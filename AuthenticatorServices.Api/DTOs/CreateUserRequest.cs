using AuthenticatorServices.Domain.Contracts;

namespace AuthenticatorServices.Api.DTOs;

public class CreateUserRequest : ICreateUserServiceRequest
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}