using System;
using System.Text.RegularExpressions;

namespace AuthenticatorServices.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private User(string name, string email, string password)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Password = password;
            CreatedAt = DateTime.UtcNow;
        }

        public static User Create(string name, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("O nome não pode ser vazio", nameof(name));

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("O email não pode ser vazio", nameof(email));

            if (!IsValidEmail(email))
                throw new ArgumentException("Email inválido", nameof(email));

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("A senha não pode ser vazia", nameof(password));

            if (password.Length < 6)
                throw new ArgumentException("A senha deve ter pelo menos 6 caracteres", nameof(password));

            return new User(name, email, password);
        }

        private static bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }
    }
} 