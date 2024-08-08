using FluentValidation;

namespace UserManagement.Application.Commands.AccessToken
{
    public class AccessTokenValidator : AbstractValidator<AccessTokenCommand>
    {
        public AccessTokenValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Please enter User name.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Please enter password.");
        }
    }
}
