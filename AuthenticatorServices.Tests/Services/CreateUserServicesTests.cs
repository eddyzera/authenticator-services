using System;
using System.Threading.Tasks;
using Xunit;
using AuthenticatorServices.Domain.Services;
using AuthenticatorServices.Domain.Contracts;
using AuthenticatorServices.Domain.Repository.Interfaces;
using AuthenticatorServices.Domain.Entities;
using AuthenticatorServices.Security.Infrastructure;
using AuthenticatorServices.Tests.Services.Mocks;

namespace AuthenticatorServices.Tests.Services
{
    public class CreateUserServicesTests
    {
        private readonly CreateUserServices _createUserServices;
        private readonly IPasswordService _passwordService;
        private readonly MockUserRepository _userRepository;

        public CreateUserServicesTests()
        {
            _passwordService = new PasswordService();
            _userRepository = new MockUserRepository();
            _createUserServices = new CreateUserServices(_passwordService, _userRepository);
        }

        [Fact]
        public async Task Execute_ShouldCreateUser_WithValidRequest()
        {
            // Arrange
            var request = new CreateUserRequest
            {
                Name = "Test User",
                Email = "test@example.com",
                Password = "TestPassword123!"
            };

            // Act
            var response = await _createUserServices.Execute(request);

            // Assert
            Assert.NotNull(response);
            Assert.NotEqual(Guid.Empty, response.Id);
            Assert.Equal(request.Name, response.Name);
            Assert.Equal(request.Email, response.Email);
            Assert.NotEqual(request.Password, response.Password); // Password should be hashed
            Assert.NotEqual(default(DateTime), response.CreatedAt);
        }

        [Fact]
        public async Task Execute_ShouldThrowException_WhenRequestIsNull()
        {
            // Arrange
            ICreateUserServiceRequest request = null;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _createUserServices.Execute(request));
        }

        [Fact]
        public async Task Execute_ShouldThrowException_WhenEmailAlreadyExists()
        {
            // Arrange
            var existingUser = User.Create("Existing User", "test@example.com", "hashedPassword");
            _userRepository.SetExistingUser(existingUser);

            var request = new CreateUserRequest
            {
                Name = "New User",
                Email = "test@example.com",
                Password = "TestPassword123!"
            };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(() => _createUserServices.Execute(request));
            Assert.Equal("Email já cadastrado", exception.Message);
        }

        [Fact]
        public async Task Execute_ShouldHashPassword_WhenCreatingNewUser()
        {
            // Arrange
            var request = new CreateUserRequest
            {
                Name = "Test User",
                Email = "test@example.com",
                Password = "TestPassword123!"
            };

            // Act
            var response = await _createUserServices.Execute(request);

            // Assert
            Assert.NotNull(response.Password);
            Assert.NotEqual(request.Password, response.Password); // Senha não deve ser igual à original
            Assert.True(response.Password.StartsWith("$2")); // BCrypt hash sempre começa com $2
            Assert.True(_passwordService.VerifyPassword(request.Password, response.Password)); // Deve ser possível verificar a senha
        }

        private class CreateUserRequest : ICreateUserServiceRequest
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
} 