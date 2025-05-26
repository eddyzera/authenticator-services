# Authenticator Services

Sistema de autenticação desenvolvido em .NET 8, seguindo os princípios da Clean Architecture e Domain-Driven Design (DDD).

## 🏗️ Estrutura do Projeto

O projeto está organizado em múltiplas camadas, cada uma com sua responsabilidade específica:

### 1. AuthenticatorServices.Domain
Camada central contendo as regras de negócio e entidades do domínio.

#### Estrutura:
```
AuthenticatorServices.Domain/
├── Entities/          # Entidades do domínio
│   └── User.cs       # Entidade de usuário
├── Services/         # Serviços do domínio
│   ├── CreateUserServices.cs     # Serviço de criação de usuário
│   └── AuthenticateUserServices.cs # Serviço de autenticação
├── Contracts/        # Interfaces de entrada/saída
│   ├── IAuthenticateUserService.cs  # Contratos de autenticação
│   └── ICreateUserService.cs        # Contratos de criação de usuário
└── Repository/       # Implementação do repositório
    └── JsonUserRepository.cs    # Repositório com persistência em JSON
```

#### Responsabilidades:
- Definições de entidades do domínio
- Implementação das regras de negócio
- Contratos de entrada/saída dos serviços
- Implementação do repositório com persistência em JSON

### 2. AuthenticatorServices.Security
Camada responsável pela segurança e criptografia.

#### Estrutura:
```
AuthenticatorServices.Security/
└── Infrastructure/
    ├── Password/           # Serviços relacionados a senha
    │   ├── IPasswordService.cs    # Interface do serviço de senha
    │   └── PasswordService.cs     # Implementação do serviço de senha
    └── Token/             # Serviços relacionados a JWT
        ├── IJwtService.cs         # Interface do serviço JWT
        ├── JwtService.cs          # Implementação do serviço JWT
        └── JwtSettings.cs         # Configuração do JWT
```

#### Responsabilidades:
- Criptografia e validação de senhas
- Geração e validação de tokens JWT
- Implementação de políticas de segurança

### 3. AuthenticatorServices.Tests
Camada de testes automatizados.

#### Estrutura:
```
AuthenticatorServices.Tests/
├── Services/           # Testes dos serviços
│   ├── CreateUserServicesTests.cs
│   └── AuthenticateUserServicesTests.cs
└── Repository/        # Testes do repositório
    └── JsonUserRepositoryTests.cs
```

### 4. AuthenticatorServices.Console
Aplicação console para demonstração e testes.

#### Funcionalidades:
- Criação de usuários
- Autenticação de usuários
- Persistência em arquivo JSON

## 🚀 Como Executar

1. Clone o repositório
2. Restaure as dependências:
   ```bash
   dotnet restore
   ```
3. Execute os testes:
   ```bash
   dotnet test
   ```
4. Execute a aplicação console:
   ```bash
   dotnet run --project AuthenticatorServices.Console
   ```

## 📝 Fluxo de Autenticação

1. **Criação de Usuário**:
   - O usuário fornece nome, email e senha
   - A senha é criptografada usando BCrypt
   - Os dados são salvos em `users.json`

2. **Autenticação**:
   - O usuário fornece email e senha
   - O sistema verifica as credenciais
   - Se válidas, gera um token JWT
   - O token contém claims de ID, email e roles

## 🔒 Segurança

- Senhas criptografadas com BCrypt
- Tokens JWT com claims personalizadas
- Validação de email
- Senhas com requisitos mínimos de segurança

## 📦 Tecnologias Utilizadas

- .NET 8
- xUnit para testes
- BCrypt.Net-Next para criptografia
- JWT Bearer Authentication
- System.Text.Json para persistência

## 🔄 Próximos Passos

- [ ] Implementar refresh token
- [ ] Adicionar autorização baseada em roles
- [ ] Implementar validação de força de senha
- [ ] Adicionar logging
- [ ] Implementar rate limiting 