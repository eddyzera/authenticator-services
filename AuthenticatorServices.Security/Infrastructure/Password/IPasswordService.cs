using System;

namespace AuthenticatorServices.Security.Infrastructure.Password
{
    public interface IPasswordService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
    }
} 