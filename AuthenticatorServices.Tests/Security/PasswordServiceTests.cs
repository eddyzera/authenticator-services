using System;
using AuthenticatorServices.Security.Infrastructure.Password;
using Xunit;

namespace AuthenticatorServices.Tests.Security
{
    public class PasswordServiceTests
    {
        private readonly IPasswordService _passwordService;

        public PasswordServiceTests()
        {
            _passwordService = new PasswordService();
        }

        [Fact]
        public void HashPassword_ShouldReturnHashedPassword()
        {
            var password = "senha123";
            var hash = _passwordService.HashPassword(password);
            Assert.False(string.IsNullOrEmpty(hash));
            Assert.NotEqual(password, hash);
        }

        [Fact]
        public void VerifyPassword_ShouldReturnTrue_WhenPasswordIsCorrect()
        {
            var password = "senha123";
            var hash = _passwordService.HashPassword(password);
            Assert.True(_passwordService.VerifyPassword(password, hash));
        }

        [Fact]
        public void VerifyPassword_ShouldReturnFalse_WhenPasswordIsIncorrect()
        {
            var password = "senha123";
            var hash = _passwordService.HashPassword(password);
            Assert.False(_passwordService.VerifyPassword("outraSenha", hash));
        }
    }
} 