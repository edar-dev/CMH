using Building.Dtos;

namespace Building.Requests;

public record CreateHouseRequest(string Name, string Description, RoomsModel[] Rooms)
{
    public HouseDto ToDto(Guid id)
    {
        return new HouseDto(id, Name, Description);
    }
}