using MediatR;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.Commands.AccessToken
{
    public class AccessTokenCommand : IRequest<TokenDto>
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
