using System;
using System.Threading.Tasks;
using AuthenticatorServices.Domain.Contracts;
using AuthenticatorServices.Domain.Entities;
using AuthenticatorServices.Domain.Repository.Interfaces;
using AuthenticatorServices.Security.Infrastructure.Password;

namespace AuthenticatorServices.Domain.Services
{
    public class CreateUserServices
    {
        private readonly IPasswordService _passwordService;
        private readonly IUserRepository _userRepository;

        public CreateUserServices(IPasswordService passwordService, IUserRepository userRepository)
        {
            _passwordService = passwordService ?? throw new ArgumentNullException(nameof(passwordService));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<ICreateUserServiceResponse> Execute(ICreateUserServiceRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
                
            User? userEmail = await _userRepository.FindByEmail(request.Email);
            if(userEmail is not null)
                throw new Exception("Email já cadastrado");
            
            string hashedPassword = _passwordService.HashPassword(request.Password);
            User user = await _userRepository.CreateUser(User.Create(request.Name, request.Email, hashedPassword));

            return new CreateUserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = hashedPassword,
                CreatedAt = user.CreatedAt
            };
        }
    }

    public class CreateUserResponse : ICreateUserServiceResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
