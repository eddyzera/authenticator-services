using System;
using System.Threading.Tasks;
using AuthenticatorServices.Domain.Contracts;
using AuthenticatorServices.Domain.Entities;
using AuthenticatorServices.Domain.Repository.Interfaces;
using AuthenticatorServices.Domain.Services;
using AuthenticatorServices.Security.Infrastructure.Password;
using AuthenticatorServices.Tests.Services.Mocks;
using Xunit;

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
        }

        [Fact]
        public async Task Execute_ShouldThrowException_WhenRequestIsNull()
        {
            // Arrange
            ICreateUserServiceRequest? request = null;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _createUserServices.Execute(request!));
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

        private class CreateUserRequest : ICreateUserServiceRequest
        {
            public string Name { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }
    }
} 