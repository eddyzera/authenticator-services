using AuthenticatorServices.Domain.Entities;
using AuthenticatorServices.Domain.Repository.Interfaces;
using AuthenticatorServices.Domain.Core.DataBaseConfig;
using Microsoft.EntityFrameworkCore;


namespace AuthenticatorServices.Domain.Repository
{
    public class EFUserRepository : IUserRepository
    {

        public async Task<User> CreateUser(User user)
        {
            DataBaseConfig context = new DataBaseConfig();

            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            return user;
        }
        
        public async Task<User?> FindByEmail(string email)
        {
            DataBaseConfig context = new DataBaseConfig();

            User? user = await context.Users.FirstOrDefaultAsync(u => u.Email == email);

            return user;
        }
    }
}