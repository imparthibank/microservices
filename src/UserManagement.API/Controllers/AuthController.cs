using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Commands.AccessToken;
using UserManagement.Application.Commands.RefreshToken;

namespace UserManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<AccessTokenCommand> _validator;
        private static Dictionary<string, string> _refreshTokens = new Dictionary<string, string>();
        public AuthController(IMediator mediator, IValidator<AccessTokenCommand> validator)
        {
            _mediator = mediator;
            _validator = validator;
        }

        [HttpPost("token")]
        public async Task<IActionResult> Login([FromBody] AccessTokenCommand command)
        {
            var result = await _validator.ValidateAsync(command);

            if (!result.IsValid)
            {
                return BadRequest(result);
            }
            var tokenResponse = await _mediator.Send(command);
            if (tokenResponse != null && !string.IsNullOrEmpty(tokenResponse.Token))
            {
                _refreshTokens[tokenResponse.RefreshToken] = command.UserName;
                return Ok(tokenResponse);
            }
            return NotFound();
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenCommand command)
        {
            if (!_refreshTokens.TryGetValue(command.RefreshToken, out var username))
                return Unauthorized();
            var tokenResponse = await _mediator.Send(command);
            _refreshTokens.Remove(command.RefreshToken);
            _refreshTokens[tokenResponse.RefreshToken] = username;
            return Ok(tokenResponse);
        }

        [Authorize]
        [HttpGet("secure")]
        public IActionResult Secure()
        {
            var username = User?.Identity?.Name;
            return Ok($"Hello, {username}. This is a secure endpoint.");
        }
    }
}
