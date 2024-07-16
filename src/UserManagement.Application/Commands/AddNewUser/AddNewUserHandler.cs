using AutoMapper;
using IdentityManagement.Application.DTOs;
using IdentityManagement.Domain.Entities;
using IdentityManagement.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.Commands.AddNewUser
{
    //public class AddNewUserHandler : IRequestHandler<AddNewUserCommand, UserResponse>
    //{
    //    private readonly IUserRepository _userRepository;
    //    private readonly IMapper _mapper;
    //    public AddNewUserHandler(IUserRepository userRepository, IMapper mapper)
    //    {
    //        _userRepository = userRepository;
    //        _mapper = mapper;
    //    }
    //    public async Task<UserResponse> Handle(AddNewUserCommand request, CancellationToken cancellationToken)
    //    {
    //        var user = new User() { Id = Guid.NewGuid(), Email = request.Email, Username = request.UserName, PasswordHash = request.Password };
    //        await _userRepository.AddAsync(user);

    //        return _mapper.Map<UserResponse>(user);
    //    }
    //}
}
