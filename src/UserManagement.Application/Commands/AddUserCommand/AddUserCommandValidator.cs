using FluentValidation;

namespace UserManagement.Application.Commands.AddUserCommand
{
    public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
    {
        public AddUserCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required.");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("A valid email is required.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Confirm Password is required.");
            RuleFor(x => x.Password).NotEmpty().When(x => !x.Equals(x.ConfirmPassword)).WithMessage("Password and Confirm Password is mismatched.");
        }
    }
}
