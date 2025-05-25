using System;
using System.Threading.Tasks;
using AuthenticatorServices.Domain.Entities;
using AuthenticatorServices.Domain.Repository;
using Xunit;

namespace AuthenticatorServices.Tests.Repository
{
    public class JsonUserRepositoryTests
    {
        private readonly JsonUserRepository _repository;

        public JsonUserRepositoryTests()
        {
            _repository = new JsonUserRepository();
        }

        [Fact]
        public Task CreateUser_ShouldAddUserToList()
        {
            // Arrange
            var user = User.Create("Test User", "test@example.com", "hashedPassword");

            // Act
            var createdUser = _repository.CreateUser(user).Result;

            // Assert
            Assert.NotNull(createdUser);
            Assert.Equal(user.Id, createdUser.Id);
            Assert.Equal(user.Email, createdUser.Email);
            return Task.CompletedTask;
        }

        [Fact]
        public Task FindByEmail_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var user = User.Create("Test User", "test@example.com", "hashedPassword");
            _repository.CreateUser(user).Wait();

            // Act
            var foundUser = _repository.FindByEmail("test@example.com").Result;

            // Assert
            Assert.NotNull(foundUser);
            Assert.Equal(user.Id, foundUser.Id);
            Assert.Equal(user.Email, foundUser.Email);
            return Task.CompletedTask;
        }

        [Fact]
        public Task FindByEmail_ShouldReturnNull_WhenUserDoesNotExist()
        {
            // Act
            var foundUser = _repository.FindByEmail("nonexistent@example.com").Result;

            // Assert
            Assert.Null(foundUser);
            return Task.CompletedTask;
        }

        [Fact]
        public Task FindByEmail_ShouldBeCaseInsensitive()
        {
            // Arrange
            var user = User.Create("Test User", "Test@Example.com", "hashedPassword");
            _repository.CreateUser(user).Wait();

            // Act
            var foundUser = _repository.FindByEmail("test@example.com").Result;

            // Assert
            Assert.NotNull(foundUser);
            Assert.Equal(user.Id, foundUser.Id);
            return Task.CompletedTask;
        }
    }
} 