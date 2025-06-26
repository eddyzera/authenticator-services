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
            return Task.FromResult(user);
        }

        public Task<User?> FindByEmail(string email)
        {
            return Task.FromResult(_existingUser);
        }
    }
} 