using System;
using System.Threading.Tasks;
using AuthenticatorServices.Domain.Contracts;
using AuthenticatorServices.Domain.Repository;
using AuthenticatorServices.Domain.Services;
using AuthenticatorServices.Security.Infrastructure.Password;
using AuthenticatorServices.Security.Infrastructure.Token;

namespace AuthenticatorServices.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                // Configuração dos serviços
                var userRepository = new JsonUserRepository();
                var passwordService = new PasswordService();
                var jwtService = new JwtService(new JwtSettings
                {
                    SecretKey = "chave_secreta_teste_123456789_32_chars!!",
                    ExpirationInMinutes = 60
                });

                // Serviços
                var createUserService = new CreateUserServices(passwordService, userRepository);
                var authenticateUserService = new AuthenticateUserServices(userRepository, passwordService, jwtService);

                // Criar usuário
                System.Console.WriteLine("=== Criando Usuário ===");
                var createRequest = new CreateUserRequest
                {
                    Name = "Usuário Teste",
                    Email = "teste@exemplo.com",
                    Password = "Senha123!"
                };

                var createdUser = await createUserService.Execute(createRequest);
                System.Console.WriteLine($"Usuário criado com sucesso!");
                System.Console.WriteLine($"Data de Criação: {createdUser.CreatedAt}");
                System.Console.WriteLine();

                // Autenticar usuário
                System.Console.WriteLine("=== Autenticando Usuário ===");
                var authRequest = new AuthenticateUserRequest
                {
                    Email = "teste@exemplo.com",
                    Password = "Senha123!"
                };

                var authResponse = await authenticateUserService.Execute(authRequest);
                System.Console.WriteLine($"Usuário autenticado com sucesso!");
                System.Console.WriteLine($"Token: {authResponse.Token}");
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Erro: {ex.Message}");
            }

            System.Console.WriteLine("\nPressione qualquer tecla para sair...");
            System.Console.ReadKey();
        }
    }

    public class CreateUserRequest : ICreateUserServiceRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class AuthenticateUserRequest : IAuthenticateUserServiceRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
