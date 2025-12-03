using AbstraTest.Regions.Core.Cities.Models;
using FluentValidation;

namespace AbstraTest.Regions.Core.Cities.Validators;

public class CityValidator : AbstractValidator<City>
{
    public CityValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .MaximumLength(100);
    }
}