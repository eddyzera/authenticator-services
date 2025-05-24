using System;

namespace AuthenticatorServices.Security.Infrastructure
{
    public class PasswordService : IPasswordService
    {
        public string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("A senha não pode ser vazia", nameof(password));

            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("A senha não pode ser vazia", nameof(password));

            if (string.IsNullOrEmpty(hashedPassword))
                throw new ArgumentException("O hash da senha não pode ser vazio", nameof(hashedPassword));

            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
} 