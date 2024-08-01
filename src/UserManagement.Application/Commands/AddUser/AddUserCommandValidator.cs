using FluentValidation;

namespace UserManagement.Application.Commands.AddUser
{
    public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
    {
        public AddUserCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Please enter First name.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Please enter Last name.");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Please enter valid email.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Please enter password.");
            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Please enter confirm password.");
            RuleFor(customer => customer.Password).Equal(customer => customer.ConfirmPassword).WithMessage("'Password' must be same as 'ConfirmPassword'");
        }
    }
}
