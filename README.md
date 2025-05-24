# Authenticator Services

Authentication system developed in .NET 8, following Clean Architecture and Domain-Driven Design (DDD) principles.

## 🏗️ Project Structure

The project is organized in multiple layers, each with its specific responsibility:

### 1. AuthenticatorServices.Domain
Central layer containing business rules and domain entities.

#### Structure:
```
AuthenticatorServices.Domain/
├── Entities/          # Domain entities
│   └── User.cs       # User entity
├── Services/         # Domain services
│   ├── CreateUserServices.cs     # User creation service
│   └── AuthenticateUserServices.cs # User authentication service
├── Contracts/        # Input/output interfaces
│   ├── IAuthenticateUserService.cs  # Authentication contracts
│   └── ICreateUserService.cs        # User creation contracts
└── Repository/       # Repository interfaces
    └── Interfaces/
        └── IUserRepository.cs
```

#### Responsibilities:
- Domain entity definitions
- Business rules implementation
- Service input/output contracts
- Repository interfaces

### 2. AuthenticatorServices.Security
Layer responsible for security and encryption.

#### Structure:
```
AuthenticatorServices.Security/
└── Infrastructure/
    ├── Password/           # Password-related services
    │   ├── IPasswordService.cs    # Password service interface
    │   └── PasswordService.cs     # Password service implementation
    └── Token/             # JWT-related services
        ├── IJwtService.cs         # JWT service interface
        ├── JwtService.cs          # JWT service implementation
        └── JwtSettings.cs         # JWT configuration
```

#### Responsibilities:
- Password encryption and validation
- JWT token generation and validation
- Security policy implementation

### 3. AuthenticatorServices.Tests
Automated testing layer.

#### Structure:
```
AuthenticatorServices.Tests/
├── Services/
│   ├── CreateUserServicesTests.cs     # User creation tests
│   ├── AuthenticateUserServicesTests.cs # Authentication tests
│   └── Mocks/
│       └── MockUserRepository.cs      # Repository mock
└── Security/
    └── PasswordServiceTests.cs        # Password service tests
```

#### Responsibilities:
- Unit tests
- Integration tests
- Test mocks and stubs

## 🛠️ Technologies Used

- .NET 8
- xUnit (for testing)
- BCrypt.Net-Next (for password encryption)
- JWT Bearer Authentication

## 🔐 Implemented Features

### 1. User Creation
- Unique email validation
- Password encryption using BCrypt
- Unique ID generation
- Creation date registration

### 2. User Authentication
- Email and password validation
- JWT token generation with user claims
- Token validation
- User information retrieval

### 3. Security
- Password hashing using BCrypt
- Password verification
- JWT token security with claims
- Credential validation
- Organized security infrastructure

## 🧪 Tests

The project includes comprehensive automated tests:

### Service Tests
- User creation with valid data
- Duplicate email validation
- Password encryption verification
- Authentication with valid credentials
- Authentication with invalid credentials
- Token generation and validation

### Security Tests
- Password hashing
- Password verification
- JWT token generation
- JWT token validation
- Null input validation

## 📦 How to Run

1. Clone the repository
2. Restore NuGet packages:
   ```bash
   dotnet restore
   ```
3. Run the tests:
   ```bash
   dotnet test
   ```

## 🔄 Authentication Flow

1. Receive authentication request with email and password
2. Validate user credentials
3. Generate JWT token with user information and roles
4. Return user data with authentication token

## 🔒 Security

- Passwords stored with BCrypt hash
- Unique email validation
- JWT token-based authentication with claims
- Exception handling
- Input validation
- Organized security infrastructure

## 📝 Next Steps

- [ ] Implement refresh token mechanism
- [ ] Add password strength validation
- [ ] Implement password recovery
- [ ] Add audit logs
- [ ] Implement rate limiting
- [ ] Add role-based authorization
- [ ] Add API documentation
- [ ] Implement logging system 