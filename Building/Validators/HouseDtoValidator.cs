using Building.Dtos;
using FluentValidation;

namespace Building.Validators;

public class HouseDtoValidator : AbstractValidator<HouseDto>
{
    public HouseDtoValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty().NotNull();
        RuleFor(x => x.Description).NotNull().NotEmpty();

    }
}