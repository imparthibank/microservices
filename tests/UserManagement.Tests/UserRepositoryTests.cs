using AutoMapper;
using FluentAssertions;
using IdentityManagement.Application.DTOs;
using IdentityManagement.Application.Handlers;
using IdentityManagement.Application.Queries;
using IdentityManagement.Domain.Entities;
using IdentityManagement.Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityManagement.Tests
{
    public class UserRepositoryTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly IMapper _mapper;

        public UserRepositoryTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDto>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetUserById_Returns_User()
        {
            var userId = Guid.NewGuid();
            var user = new User { Id = userId, Username = "TestUser", Email = "test@example.com" };
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(user);

            var handler = new GetUserByIdHandler(_userRepositoryMock.Object, _mapper);
            var result = await handler.Handle(new GetUserByIdQuery { Id = userId }, default);

            result.Should().NotBeNull();
            result.Username.Should().Be(user.Username);
            result.Email.Should().Be(user.Email);
        }
    }
}
