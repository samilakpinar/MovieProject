using Entities.Concrete;
using FluentValidation;

namespace MovieProject.Validators
{
    public class UsersValidator : AbstractValidator<Users>
    {
        public UsersValidator()
        {
            RuleFor(user => user.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Please enter the user name");

            RuleFor(user => user.Surname)
                .NotEmpty()
                .NotNull()
                .WithMessage("Please enter the surname");

            RuleFor(user => user.Email)
                .NotEmpty()
                .NotNull()
                .EmailAddress()
                .WithMessage("Please enter the email");

            RuleFor(user => user.Password)
                .NotNull()
                .NotEmpty()
                .WithMessage("Please enter the password")
                .Must(password => password.Length >= 8)
                .WithMessage("Password must be 8 characters");
        }
    }
}
