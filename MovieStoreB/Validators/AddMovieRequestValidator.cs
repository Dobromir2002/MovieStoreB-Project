using FluentValidation;
using MovieStoreB.Models.Requests;

namespace MovieStoreB.Validators
{
    public class AddMovieRequestValidator : AbstractValidator<AddMovieRequest>
    {
        public AddMovieRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Заглавието е задължително")
                .MinimumLength(2).WithMessage("Минимална дължина: 2 символа")
                .MaximumLength(100).WithMessage("Максимална дължина: 100 символа");

            RuleFor(x => x.Year)
                .GreaterThan(1800).WithMessage("Годината трябва да е след 1800");

            RuleFor(x => x.Genre)
                .NotEmpty().WithMessage("Жанрът е задължителен");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Описанието е задължително");

            RuleFor(x => x.Rating)
                .InclusiveBetween(0.0, 10.0).WithMessage("Рейтингът трябва да е между 0 и 10");

            RuleFor(x => x.ActorIds)
                .NotNull().WithMessage("Добави поне един актьор")
                .Must(a => a.Count > 0).WithMessage("Добави поне един актьор");
        }
    }
}

