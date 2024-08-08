using MediatR;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.Commands.RefreshToken
{
    public class RefreshTokenCommand : IRequest<TokenDto>
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }
}
