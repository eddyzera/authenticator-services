# AuthenticatorServices API

## Endpoints

### POST /api/auth/register
Registra um novo usuário.

**Request:**
```json
{
  "name": "João Silva",
  "email": "joao@email.com",
  "password": "senha123"
}
```

**Response:**
```json
{
  "id": "guid",
  "name": "João Silva",
  "email": "joao@email.com"
}
```

### POST /api/auth/login
Autentica um usuário.

**Request:**
```json
{
  "email": "joao@email.com",
  "password": "senha123"
}
```

**Response:**
```json
{
  "token": "jwt-token-here"
}
```

## Como executar

```bash
dotnet run --project AuthenticatorServices.Api
```

A API estará disponível em `https://localhost:7000` ou `http://localhost:5000`.