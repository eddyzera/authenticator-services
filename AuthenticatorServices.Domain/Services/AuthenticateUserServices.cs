using System;
using System.Threading.Tasks;
using AuthenticatorServices.Domain.Contracts;
using AuthenticatorServices.Domain.Repository.Interfaces;
using AuthenticatorServices.Security.Infrastructure.Password;
using AuthenticatorServices.Security.Infrastructure.Token;

namespace AuthenticatorServices.Domain.Services
{
    public class AuthenticateUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;
        private readonly IJwtService _jwtService;

        public AuthenticateUserServices(
            IUserRepository userRepository,
            IPasswordService passwordService,
            IJwtService jwtService)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _passwordService = passwordService ?? throw new ArgumentNullException(nameof(passwordService));
            _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
        }

        public async Task<IAuthenticateUserServiceResponse> Execute(IAuthenticateUserServiceRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var user = await _userRepository.FindByEmail(request.Email);
            if (user == null)
                throw new Exception("Usuário não encontrado");

            if (!_passwordService.VerifyPassword(request.Password, user.Password))
                throw new Exception("Senha inválida");

            var token = _jwtService.GenerateToken(
                userId: user.Id.ToString(),
                email: user.Email,
                roles: new[] { "User" } // Aqui você pode adicionar as roles do usuário
            );

            return new AuthenticateUserResponse
            {
                Token = token
            };
        }
    }

    public class AuthenticateUserResponse : IAuthenticateUserServiceResponse
    {
        public string Token { get; set; } = string.Empty;
    }
}