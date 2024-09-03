using IdentityManagement.Domain.Interfaces;
using MediatR;
using UserManagement.Application.DTOs;
using UserManagement.Application.Services;
using UserManagement.Application.Utils;

namespace UserManagement.Application.Commands.AccessToken
{
    public class AccessTokenCommandHandler : IRequestHandler<AccessTokenCommand, TokenDto>
    {
        private readonly TokenService _tokenService;
        private readonly IUserRepository _userRepository;

        public AccessTokenCommandHandler(TokenService tokenService, IUserRepository userRepository)
        {
            _tokenService = tokenService;
            _userRepository = userRepository;
        }

        public async Task<TokenDto> Handle(AccessTokenCommand request, CancellationToken cancellationToken)
        {
            var tokenResponse = new TokenDto();
            var user = await _userRepository.GetUserByUserNameAsync(request.UserName, cancellationToken);
            if (user == null || !PasswordHasher.VerifyPassword(request.Password, user.Salt, user.PasswordHash))
            {
                return tokenResponse;
            }
            var token = _tokenService.GenerateToken(request.UserName);
            var refreshToken = _tokenService.GenerateRefreshToken();
            tokenResponse.Token = token;
            tokenResponse.RefreshToken = refreshToken;
            return tokenResponse;
        }
    }
}
