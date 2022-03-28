using Building.Dtos;

namespace Building.Requests;

public record UpdateRoomRequest(Guid Id, string Name, double Dimension, Guid HouseId)
{
    public RoomDto ToDto()
    {
        return new RoomDto(Id, Name, Dimension, HouseId);
    }
}