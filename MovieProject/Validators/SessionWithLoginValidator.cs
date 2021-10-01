using Business.Models;
using FluentValidation;

namespace MovieProject.Validators
{
    public class SessionWithLoginValidator : AbstractValidator<SessionWithLogin>
    {
        public SessionWithLoginValidator()
        {
            RuleFor(session => session.username)
                .NotNull()
                .NotEmpty()
                .WithMessage("Please enter the username");

            RuleFor(session => session.password)
                .NotNull()
                .NotEmpty()
                .WithMessage("Please enter the password");

            RuleFor(session => session.request_token)
                .NotNull()
                .NotEmpty()
                .WithMessage("Please enter the token");
        }
    }
}
