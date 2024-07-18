using AutoMapper;
using IdentityManagement.Domain.Entities;
using IdentityManagement.Domain.Interfaces;
using MediatR;

namespace UserManagement.Application.Commands.AddUserCommand
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public AddUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<Guid> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            //var user = new User() { Id = Guid.NewGuid(), Email = request.Email, Username = request.UserName, PasswordHash = request.Password };
            // Map the command to the User entity
            var user = _mapper.Map<User>(request);
            await _userRepository.AddAsync(user);
            return user.Id;
        }
    }
}
