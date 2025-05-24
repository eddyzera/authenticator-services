using System;
using System.Threading.Tasks;
using AuthenticatorServices.Domain.Contracts;
using AuthenticatorServices.Domain.Services;
using AuthenticatorServices.Security.Infrastructure.Password;
using AuthenticatorServices.Security.Infrastructure.Token;
using AuthenticatorServices.Tests.Services.Mocks;
using Xunit;

namespace AuthenticatorServices.Tests.Services
{
    public class AuthenticateUserServicesTests
    {
        private readonly MockUserRepository _userRepository;
        private readonly PasswordService _passwordService;
        private readonly JwtService _jwtService;
        private readonly AuthenticateUserServices _authenticateUserServices;

        public AuthenticateUserServicesTests()
        {
            _userRepository = new MockUserRepository();
            _passwordService = new PasswordService();
            _jwtService = new JwtService(new JwtSettings
            {
                SecretKey = "chave_secreta_teste_123456789_32_chars!!",
                Issuer = "test_issuer",
                Audience = "test_audience",
                ExpirationInMinutes = 60
            });
            _authenticateUserServices = new AuthenticateUserServices(
                _userRepository,
                _passwordService,
                _jwtService
            );
        }

        [Fact]
        public async Task Execute_ShouldReturnToken_WhenCredentialsAreValid()
        {
            // Arrange
            var password = "senha123";
            var hashedPassword = _passwordService.HashPassword(password);
            var user = Domain.Entities.User.Create(
                "Test User",
                "test@example.com",
                hashedPassword
            );
            _userRepository.SetExistingUser(user);

            var request = new AuthenticateUserRequest
            {
                Email = "test@example.com",
                Password = password
            };

            // Act
            var response = await _authenticateUserServices.Execute(request);

            // Assert
            Assert.NotNull(response);
            Assert.NotNull(response.Token);
            Assert.NotEmpty(response.Token);
        }

        [Fact]
        public async Task Execute_ShouldThrowException_WhenUserNotFound()
        {
            // Arrange
            var request = new AuthenticateUserRequest
            {
                Email = "nonexistent@example.com",
                Password = "senha123"
            };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _authenticateUserServices.Execute(request));
            Assert.Equal("Usuário não encontrado", exception.Message);
        }

        [Fact]
        public async Task Execute_ShouldThrowException_WhenPasswordIsInvalid()
        {
            // Arrange
            var user = Domain.Entities.User.Create(
                "Test User",
                "test@example.com",
                _passwordService.HashPassword("senha123")
            );
            _userRepository.SetExistingUser(user);

            var request = new AuthenticateUserRequest
            {
                Email = "test@example.com",
                Password = "senha_errada"
            };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _authenticateUserServices.Execute(request));
            Assert.Equal("Senha inválida", exception.Message);
        }

        [Fact]
        public async Task Execute_ShouldThrowException_WhenRequestIsNull()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _authenticateUserServices.Execute(null));
        }
    }

    public class AuthenticateUserRequest : IAuthenticateUserServiceRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
} 