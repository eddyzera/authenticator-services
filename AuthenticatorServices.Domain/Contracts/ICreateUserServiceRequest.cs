using System;

namespace AuthenticatorServices.Domain.Contracts
{
    public interface ICreateUserServiceRequest
    {
        string Name { get; }
        string Email { get; }
        string Password { get; }
    }
} 