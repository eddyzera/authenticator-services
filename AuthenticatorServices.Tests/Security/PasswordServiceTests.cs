using System;
using Xunit;
using AuthenticatorServices.Security.Infrastructure;

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
        public void HashPassword_ShouldNotReturnOriginalPassword()
        {
            // Arrange
            string password = "TestPassword123!";

            // Act
            string hashedPassword = _passwordService.HashPassword(password);

            // Assert
            Assert.NotEqual(password, hashedPassword);
            Assert.NotNull(hashedPassword);
        }

        [Fact]
        public void HashPassword_ShouldThrowException_WhenPasswordIsNull()
        {
            // Arrange
            string password = null;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _passwordService.HashPassword(password));
        }

        [Fact]
        public void HashPassword_ShouldThrowException_WhenPasswordIsEmpty()
        {
            // Arrange
            string password = string.Empty;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _passwordService.HashPassword(password));
        }

        [Fact]
        public void VerifyPassword_ShouldReturnTrue_WhenPasswordMatches()
        {
            // Arrange
            string password = "TestPassword123!";
            string hashedPassword = _passwordService.HashPassword(password);

            // Act
            bool result = _passwordService.VerifyPassword(password, hashedPassword);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void VerifyPassword_ShouldReturnFalse_WhenPasswordDoesNotMatch()
        {
            // Arrange
            string password = "TestPassword123!";
            string wrongPassword = "WrongPassword123!";
            string hashedPassword = _passwordService.HashPassword(password);

            // Act
            bool result = _passwordService.VerifyPassword(wrongPassword, hashedPassword);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void VerifyPassword_ShouldThrowException_WhenPasswordIsNull()
        {
            // Arrange
            string password = null;
            string hashedPassword = "hashedPassword";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _passwordService.VerifyPassword(password, hashedPassword));
        }

        [Fact]
        public void VerifyPassword_ShouldThrowException_WhenHashedPasswordIsNull()
        {
            // Arrange
            string password = "TestPassword123!";
            string hashedPassword = null;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _passwordService.VerifyPassword(password, hashedPassword));
        }
    }
} 