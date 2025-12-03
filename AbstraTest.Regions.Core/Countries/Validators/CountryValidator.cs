using AbstraTest.Regions.Core.Countries.Modesl;
using FluentValidation;

namespace AbstraTest.Regions.Core.Countries.Validators;

public class CountryValidator : AbstractValidator<Country>
{
    public CountryValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .MaximumLength(100);
    }
}