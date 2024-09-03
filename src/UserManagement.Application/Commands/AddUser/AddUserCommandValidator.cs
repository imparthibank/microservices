using FluentValidation;
using IdentityManagement.Domain.Interfaces;

namespace UserManagement.Application.Commands.AddUser
{
    public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
    {
        private readonly IUserRepository _userRepository;
        public AddUserCommandValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(x => x.FirstName).NotEmpty().MinimumLength(5).WithMessage("Please enter First name.");
            RuleFor(x => x.LastName).NotEmpty().MinimumLength(1).WithMessage("Please enter Last name.");
            RuleFor(x => x.UserName).NotEmpty().MaximumLength(50).MustAsync(BeUniqueUserName).WithMessage("The specified username already exists.");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().MustAsync(BeUniqueEmail).WithMessage("The specified email already exists.");
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8).WithMessage("Please enter password.");
            RuleFor(x => x.ConfirmPassword).NotEmpty().MinimumLength(8).WithMessage("Please enter confirm password.");
            RuleFor(customer => customer.Password).Equal(customer => customer.ConfirmPassword).WithMessage("'Password' must be same as 'ConfirmPassword'");
        }

        // Custom validation rule to check for unique username
        private async Task<bool> BeUniqueUserName(string userName, CancellationToken cancellationToken)
        {
            return await _userRepository.IsUserNameUniqueAsync(userName, cancellationToken);
        }

        // Custom validation rule to check for unique email
        private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
        {
            return await _userRepository.IsEmailUniqueAsync(email, cancellationToken);
        }
    }
}
