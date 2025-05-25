using System;

namespace AuthenticatorServices.Domain.Contracts
{
    public interface ICreateUserServiceRequest
    {
        string Name { get; set; }
        string Email { get; set; }
        string Password { get; set; }
    }

    public interface ICreateUserServiceResponse
    {
        string Password { get; set; }
        DateTime CreatedAt { get; set; }
    }
} 