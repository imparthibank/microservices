using MediatR;

namespace UserManagement.Application.Commands.AddUser
{
    public class AddUserCommand : IRequest<Guid>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? Email { get; set; }
    }
}
