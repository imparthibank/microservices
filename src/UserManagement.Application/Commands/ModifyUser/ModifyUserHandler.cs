using AutoMapper;
using IdentityManagement.Domain.Entities;
using IdentityManagement.Domain.Interfaces;
using MediatR;

namespace UserManagement.Application.Commands.ModifyUser
{
    internal class ModifyUserHandler : IRequestHandler<ModifyUserCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public ModifyUserHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task Handle(ModifyUserCommand request, CancellationToken cancellationToken)
        {
            var user = _mapper.Map<User>(request);
            await _userRepository.UpdateAsync(user);
        }
    }
}
