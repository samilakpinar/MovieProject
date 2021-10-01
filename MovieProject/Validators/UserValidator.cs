using Business.Models;
using FluentValidation;

namespace MovieProject.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.Email)
                .NotNull()
                .NotEmpty()
                .WithMessage("Please enter the email address")
                .EmailAddress()
                .WithMessage("The email address is invalid");

            RuleFor(user => user.Password)
                .NotNull()
                .NotEmpty()
                .WithMessage("Please enter the password");
                
        }
    }
}
