using Microsoft.EntityFrameworkCore;
using AuthenticatorServices.Domain.Entities;
namespace AuthenticatorServices.Domain.Core.DataBaseConfig
{
    public class DataBaseConfig: DbContext
    {
        public DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(@"Host=localhost;Port=5432;Username=devuser;Password=devpassword;Database=devdb");
    }
}