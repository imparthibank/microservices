using FluentValidation;
using FluentValidation.Results;
using IdentityManagement.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Commands.AddUser;
using UserManagement.Application.Commands.ModifyUser;
using UserManagement.Application.Queries.GetAllUsers;
using UserManagement.Application.Queries.GetUserById;

namespace IdentityManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<AddUserCommand> _addUserCommandValidator;

        public UsersController(IMediator mediator, IValidator<AddUserCommand> addUserCommandValidator)                               
        {
            _mediator = mediator;
            _addUserCommandValidator = addUserCommandValidator;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] AddUserCommand command)
        {
            ValidationResult result = await _addUserCommandValidator.ValidateAsync(command);

            if (!result.IsValid)
            {
                return BadRequest(result);
            }

            var userId = await _mediator.Send(command);

            return Ok(userId);
        }

        [HttpPut]
        public async Task<IActionResult> ModifyUser([FromBody] ModifyUserCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetById(Guid id)
        {
            var request = new GetUserByIdQuery { Id = id };

            var user = await _mediator.Send(request);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _mediator.Send(new GetAllUsersQuery());
            return Ok(products);
        }
    }
}
