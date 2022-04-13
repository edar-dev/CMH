using Building.Dtos;
using FluentValidation;

namespace Building.Validators;

public class RoomDtoValidator : AbstractValidator<RoomDto>
{
    public RoomDtoValidator()
    {
        RuleFor(x => x.Id).NotNull().NotEmpty();
        RuleFor(x => x.Name).NotNull().NotEmpty();
        RuleFor(x => x.Dimension).NotNull();
        RuleFor(x => x.HouseId).NotNull().NotEmpty();

    }
}