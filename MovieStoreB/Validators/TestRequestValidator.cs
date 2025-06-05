using FluentValidation;
using MovieStoreB.Models.DTO;

namespace MovieStoreB.Validators
{
    public class TestRequestValidator : AbstractValidator<Movie>
    {
        public TestRequestValidator()
        {
            RuleFor(x => x.Year)
                .GreaterThan(0)
                .WithMessage("Въведи по-голяма от 0 година");

            RuleFor(x => x.ActorIds)
                .NotNull()
                .NotEmpty()
                .WithMessage("Добави поне един актьор");
        }
    }
}
