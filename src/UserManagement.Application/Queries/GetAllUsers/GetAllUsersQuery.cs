using IdentityManagement.Application.DTOs;
using MediatR;

namespace UserManagement.Application.Queries.GetAllUsers
{
    public record GetAllUsersQuery : IRequest<List<UserDto>>;
}
