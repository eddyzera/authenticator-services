using System;
using BCrypt.Net;

namespace AuthenticatorServices.Security.Infrastructure.Password
{
    public class PasswordService : IPasswordService
    {
        public string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Password cannot be null or empty.");
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hashedPassword))
                throw new ArgumentException("Password and hashedPassword cannot be null or empty.");
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
} 