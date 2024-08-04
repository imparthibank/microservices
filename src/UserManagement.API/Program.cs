using FluentValidation;
using IdentityManagement.Domain.Interfaces;
using IdentityManagement.Infrastructure.Data;
using IdentityManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using UserManagement.Application.Commands.AddUser;
using UserManagement.Application.Mapping;
using UserManagement.Application.Queries.GetUserById;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<IdentityManagementDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserRepository, UserRepository>();

// Configure MediatR
builder.Services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(typeof(AddUserCommandHandler).Assembly));

// Configure AutoMapper
//builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddAutoMapper(typeof(MappingProfile));


builder.Services.AddTransient<IValidator<AddUserCommand>, AddUserCommandValidator>();
builder.Services.AddTransient<IValidator<GetUserByIdQuery>, GetUserByIdValidator>();

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
