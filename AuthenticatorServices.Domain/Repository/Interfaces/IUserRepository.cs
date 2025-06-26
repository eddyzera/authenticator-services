using System.Threading.Tasks;
using AuthenticatorServices.Domain.Entities;

namespace AuthenticatorServices.Domain.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateUser(User user);
        Task<User?> FindByEmail(string email);
    }
} 