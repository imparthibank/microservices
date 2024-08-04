using FluentValidation;

namespace UserManagement.Application.Queries.GetUserById
{
    public class GetUserByIdValidator : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Please enter valid Id.");
        }
    }
}
