using MediatR;
using UserManagement.Application.DTOs;
using UserManagement.Application.Services;

namespace UserManagement.Application.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, TokenDto>
    {
        private readonly TokenService _tokenService;
        public RefreshTokenCommandHandler(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public async Task<TokenDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var principal = _tokenService.GetPrincipalFromExpiredToken(request.Token);
            var newToken = _tokenService.GenerateToken(principal.Identity.Name);
            var newRefreshToken = _tokenService.GenerateRefreshToken();
            return new TokenDto
            {
                Token = newToken,
                RefreshToken = newRefreshToken
            };       
        }
    }
}
