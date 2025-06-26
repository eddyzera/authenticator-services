using System;
using System.Threading.Tasks;
using AuthenticatorServices.Domain.Entities;
using AuthenticatorServices.Domain.Repository.Interfaces;

namespace AuthenticatorServices.Tests.Services.Mocks
{
    public class MockUserRepository : IUserRepository
    {
        private User? _existingUser;

        public void SetExistingUser(User user)
        {
            _existingUser = user;
        }

        public Task<User> CreateUser(User user)
        {
            // Simula o comportamento do banco gerando um ID
            var userWithId = User.Create(user.Name, user.Email, user.Password);
            var idField = typeof(User).GetField("<Id>k__BackingField", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            idField?.SetValue(userWithId, Guid.NewGuid());
            return Task.FromResult(userWithId);
        }

        public Task<User?> FindByEmail(string email)
        {
            return Task.FromResult(_existingUser);
        }
    }
} 