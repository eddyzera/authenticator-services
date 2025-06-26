using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AuthenticatorServices.Domain.Entities;
using AuthenticatorServices.Domain.Repository.Interfaces;

namespace AuthenticatorServices.Domain.Repository
{
    public class JsonUserRepository : IUserRepository
    {
        private readonly string _jsonFilePath;
        private List<User> _users;

        public JsonUserRepository(string jsonFilePath = "users.json")
        {
            _jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), jsonFilePath);
            _users = LoadUsers();
        }

        public async Task<User> CreateUser(User user)
        {
            _users.Add(user);
            await SaveUsers();
            return user;
        }

        public Task<User?> FindByEmail(string email)
        {
            return Task.FromResult(_users.FirstOrDefault(user => user.Email.Equals(email, StringComparison.OrdinalIgnoreCase)));
        }

        private List<User> LoadUsers()
        {
            if (!File.Exists(_jsonFilePath))
                return new List<User>();
            try
            {
                string json = File.ReadAllText(_jsonFilePath);
                return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
            }
            catch
            {
                return new List<User>();
            }
        }

        private async Task SaveUsers()
        {
            string json = JsonSerializer.Serialize(_users, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_jsonFilePath, json);
        }
    }
} 