using Building.Dtos;
using FluentValidation;

namespace Building.Validators;

public class RoomsDtoValidator : AbstractValidator<IEnumerable<RoomDto>>
{
    public RoomsDtoValidator()
    {
        RuleForEach(x => x).SetValidator(new RoomDtoValidator());
    }
}