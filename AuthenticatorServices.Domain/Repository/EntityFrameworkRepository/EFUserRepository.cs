using AuthenticatorServices.Domain.Entities;
using AuthenticatorServices.Domain.Repository.Interfaces;
using AuthenticatorServices.Domain.Core.DataBaseConfig;


namespace AuthenticatorServices.Domain.Repository
{
    class EFUserRepository : IUserRepository
    {

        public async Task<User> CreateUser(User user)
        {
            var context = new DataBaseConfig();

            await context.Users.AddAsync(user);

            return user;
        }
        
        public async Task<User?> FindByEmail(string email)
        {
            var context = new DataBaseConfig();

            var user = await context.Users.FindAsync(email);

            return user;
        }
    }
}