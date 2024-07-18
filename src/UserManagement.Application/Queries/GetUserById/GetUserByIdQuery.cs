using IdentityManagement.Application.DTOs;
using MediatR;

namespace UserManagement.Application.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<UserDto>
    {
        public Guid Id { get; set; }
    }
}
