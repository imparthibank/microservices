using FluentValidation;
using IdentityManagement.Domain.Interfaces;
using IdentityManagement.Infrastructure.Data;
using IdentityManagement.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using UserManagement.Application.Commands.AddUser;
using UserManagement.Application.Commands.ModifyUser;
using UserManagement.Application.Commands.AccessToken;
using UserManagement.Application.Mapping;
using UserManagement.Application.Queries.GetUserById;
using UserManagement.Application.Services;
using UserManagement.Application.Commands.RefreshToken;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<IdentityManagementDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();

// Configure MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(AddUserCommandHandler).Assembly));

// Configure AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));


builder.Services.AddTransient<IValidator<AddUserCommand>, AddUserCommandValidator>();
builder.Services.AddTransient<IValidator<ModifyUserCommand>, ModifyUserCommandValidator>();
builder.Services.AddTransient<IValidator<GetUserByIdQuery>, GetUserByIdValidator>();

builder.Services.AddTransient<IValidator<AccessTokenCommand>, AccessTokenValidator>();
builder.Services.AddTransient<IValidator<RefreshTokenCommand>, RefreshTokenValidator>();

var key = builder.Configuration["Jwt:Key"];
builder.Services.AddSingleton(new TokenService(key, builder.Configuration["Jwt:Issuer"]));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
    };
});

builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
