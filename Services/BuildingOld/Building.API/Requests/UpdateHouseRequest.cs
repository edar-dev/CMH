using Building.Dtos;

namespace Building.Requests;

public record UpdateHouseRequest(Guid Id, string Name, string Description)
{
    public HouseDto ToDto()
    {
        return new HouseDto(Id, Name, Description);
    }
}
