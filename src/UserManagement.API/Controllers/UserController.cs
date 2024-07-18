using IdentityManagement.Application.DTOs;
using UserManagement.Application.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Commands.AddUserCommand;

namespace IdentityManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] AddUserCommand command)
        {
            if (command == null)
            {
                return BadRequest();
            }

            var userId = await _mediator.Send(command);

            return Ok(userId);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetById(Guid id)
        {
            var user = await _mediator.Send(new GetUserByIdQuery { Id = id });
            return Ok(user);
        }
    }
}
