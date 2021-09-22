using Business.Models;
using FluentValidation;

namespace MovieProject.Validators
{
    public class RateMovieValidator : AbstractValidator<RateMovie>
    {
        public RateMovieValidator()  
        {
            RuleFor(x => x.MovieId)
                .NotNull()
                .NotEmpty()
                .WithMessage("Please enter the movie id!");

            RuleFor(x => x.value)
                .Must(x => x >= 1 && x <= 10)
                .WithMessage("The entered value must be between 1 and 10");
                  
        }
    }
}
