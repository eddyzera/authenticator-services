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
│   └── CreateUserServices.cs  # User creation service
├── Contracts/        # Input/output interfaces
│   ├── ICreateUserServiceRequest.cs
│   └── ICreateUserServiceResponse.cs
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
    ├── IPasswordService.cs    # Password service interface
    └── PasswordService.cs     # Password service implementation
```

#### Responsibilities:
- Password encryption
- Credential validation
- Security policy implementation

### 3. AuthenticatorServices.Tests
Automated testing layer.

#### Structure:
```
AuthenticatorServices.Tests/
├── Services/
│   ├── CreateUserServicesTests.cs  # User service tests
│   └── Mocks/
│       └── MockUserRepository.cs   # Repository mock
└── Security/
    └── PasswordServiceTests.cs     # Password service tests
```

#### Responsibilities:
- Unit tests
- Integration tests
- Test mocks and stubs

## 🛠️ Technologies Used

- .NET 8
- xUnit (for testing)
- BCrypt.Net-Next (for password encryption)

## 🔐 Implemented Features

### 1. User Creation
- Unique email validation
- Password encryption
- Unique ID generation
- Creation date registration

### 2. Security
- Password hashing using BCrypt
- Password verification
- Credential validation

## 🧪 Tests

The project includes automated tests to ensure code quality:

### Service Tests
- User creation with valid data
- Duplicate email validation
- Password encryption verification

### Security Tests
- Password hashing
- Password verification
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

## 🔄 User Creation Flow

1. Receive user data request
2. Check if email is already registered
3. Encrypt password
4. Create user in repository
5. Return created user data

## 🔒 Security

- Passwords stored with BCrypt hash
- Unique email validation
- Exception handling
- Input validation

## 📝 Next Steps

- [ ] Implement JWT authentication
- [ ] Add password strength validation
- [ ] Implement password recovery
- [ ] Add audit logs
- [ ] Implement rate limiting 