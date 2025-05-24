using System;

namespace AuthenticatorServices.Security.Infrastructure
{
    public interface IPasswordService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
    }
} 