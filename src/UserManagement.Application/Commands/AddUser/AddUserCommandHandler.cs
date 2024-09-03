using AutoMapper;
using IdentityManagement.Domain.Entities;
using IdentityManagement.Domain.Interfaces;
using MediatR;
using UserManagement.Application.Utils;

namespace UserManagement.Application.Commands.AddUser
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
            var salt = PasswordHasher.GenerateSalt();
            request.Password = PasswordHasher.HashPassword(request.Password, salt);
            request.Salt = salt;
            var user = _mapper.Map<User>(request);
            await _userRepository.AddAsync(user);
            return user.Id;
        }
    }
}
