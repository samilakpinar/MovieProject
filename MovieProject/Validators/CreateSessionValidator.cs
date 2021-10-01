using Business.Models;
using FluentValidation;

namespace MovieProject.Validators
{
    public class CreateSessionValidator : AbstractValidator<CreateSession>
    {
        public CreateSessionValidator()
        {
            RuleFor(session => session.request_token)
                .NotNull()
                .NotEmpty()
                .WithMessage("Please enter the token value");
        }
    }
}
