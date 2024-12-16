using FluentValidation;

namespace MoviesApp.Application.Movies.Commands.CreateMovie
{
    public class CreateMovieCommandValidator : AbstractValidator<CreateMovieCommand>
    {
        public CreateMovieCommandValidator()
        {
            RuleFor(m => m.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(200).WithMessage("Title should have maximum 200 characters");

            RuleFor(m => m.Year)
                .NotEmpty().WithMessage("Year is required")
                .LessThan(2201).WithMessage("Max year is 2200")
                .GreaterThan(1900).WithMessage("Minimum year is 1900");

        }
    }
}
