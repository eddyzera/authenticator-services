using System;

namespace AuthenticatorServices.Domain.Contracts
{
    public interface IAuthenticateUserServiceRequest
    {
        string Email { get; set; }
        string Password { get; set; }
    }

    public interface IAuthenticateUserServiceResponse
    {
        string Token { get; set; }
    }
}