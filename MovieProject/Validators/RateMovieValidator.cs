using Business.Models;
using FluentValidation;

namespace MovieProject.Validators
{
    public class RateMovieValidator : AbstractValidator<RateMovie>
    {
        public RateMovieValidator()  
        {
            RuleFor(x => x.MovieId).NotNull().NotEmpty().WithMessage("Please enter the movie id");
        }
    }
}
