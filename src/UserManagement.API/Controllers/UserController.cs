using FluentValidation;
using FluentValidation.Results;
using IdentityManagement.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Commands.AddUser;
using UserManagement.Application.Queries.GetUserById;

namespace IdentityManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<AddUserCommand> _validator;


        public UsersController(IMediator mediator, IValidator<AddUserCommand> validator)
        {
            _mediator = mediator;
            _validator = validator;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] AddUserCommand command)
        {
            ValidationResult result = await _validator.ValidateAsync(command);

            if (!result.IsValid)
            {
                return BadRequest(result);
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
