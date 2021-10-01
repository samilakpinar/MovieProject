using Business.Models;
using FluentValidation;

namespace MovieProject.Validators
{
    public class ResetPasswordValidator : AbstractValidator<ResetPassword>
    {
        public ResetPasswordValidator()
        {
            RuleFor(reset => reset.Email)
                .NotNull()
                .NotEmpty()
                .WithMessage("Please enter the email address")
                .EmailAddress()
                .WithMessage("The email address is invalid");

            RuleFor(reset => reset.Password)
                .NotNull()
                .NotEmpty()
                .WithMessage("Please enter the password");

        }
    }
}
