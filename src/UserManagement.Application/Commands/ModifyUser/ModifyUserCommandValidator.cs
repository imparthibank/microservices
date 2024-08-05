using FluentValidation;

namespace UserManagement.Application.Commands.ModifyUser
{
    public class ModifyUserCommandValidator : AbstractValidator<ModifyUserCommand>
    {
        public ModifyUserCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Please enter valid Id.");
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Please enter First name.");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Please enter Last name.");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Please enter valid email.");
        }
    }
}
