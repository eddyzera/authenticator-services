using AuthenticatorServices.Domain.Services;
using AuthenticatorServices.Domain.Repository;
using AuthenticatorServices.Domain.Repository.Interfaces;
using AuthenticatorServices.Security.Infrastructure.Password;
using AuthenticatorServices.Security.Infrastructure.Token;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// JWT Configuration
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));

// Dependency Injection
builder.Services.AddScoped<CreateUserServices>();
builder.Services.AddScoped<AuthenticateUserServices>();
builder.Services.AddScoped<IUserRepository, EFUserRepository>();
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<IJwtService, JwtService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
