using FluentValidation;

namespace UserManagement.Application.Commands.RefreshToken
{
    public class RefreshTokenValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenValidator()
        {
            RuleFor(x => x.Token).NotEmpty().WithMessage("Please enter token.");
            RuleFor(x => x.RefreshToken).NotEmpty().WithMessage("Please enter refresh token.");
        }
    }
}
