using FluentValidation.AspNetCore;
using IdentityManagement.Domain.Interfaces;
using IdentityManagement.Infrastructure.Data;
using IdentityManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using UserManagement.Application.Commands.AddUserCommand;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<IdentityManagementDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();

// Configure MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly));

// Configure AutoMapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// Configure FluentValidation
builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AddUserCommandValidator>());

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
