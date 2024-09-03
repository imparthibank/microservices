using MediatR;
using System.Text.Json.Serialization;

namespace UserManagement.Application.Commands.AddUser
{
    public class AddUserCommand : IRequest<Guid>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        [JsonIgnore]
        public byte[]? Salt { get; set; }
        public string? ConfirmPassword { get; set; }
        public string? Email { get; set; }
    }
}
