using Business.Models;
using FluentValidation;

namespace MovieProject.Validators
{
    public class ValidationEmailValidator : AbstractValidator<ValidationEmail>
    {
        public ValidationEmailValidator()
        {
            RuleFor(email => email.Email)
                .NotEmpty()
                .NotNull()
                .WithMessage("Please enter the email address")
                .EmailAddress()
                .WithMessage("The email address is invalid");

            RuleFor(email => email.Token)
                .NotNull()
                .NotEmpty()
                .WithMessage("Please enter the token value");
        }
    }
}
