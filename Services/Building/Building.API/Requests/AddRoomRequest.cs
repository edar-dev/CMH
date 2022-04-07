using Building.Dtos;

namespace Building.Requests;

public record AddRoomRequest(Guid HouseId, string Name, double Dimension)
{
    public RoomDto ToRoomDto(Guid roomId)
    {
        return new RoomDto(roomId, Name, Dimension, HouseId);
    }
}
