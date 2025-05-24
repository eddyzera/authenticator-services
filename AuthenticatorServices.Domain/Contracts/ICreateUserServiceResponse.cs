using System;

namespace AuthenticatorServices.Domain.Contracts
{
    public interface ICreateUserServiceResponse
    {
        Guid Id { get; }
        string Name { get; }
        string Email { get; }
        string Password { get; }
        DateTime CreatedAt { get; }
    }
} 