using Building.Dtos;

namespace Building.Requests
{
    public record RoomsModel(string Name, double Dimension)
    {
        public RoomDto ToDto(Guid id, Guid houseId)
        {
            return new RoomDto(id, Name, Dimension, houseId);
        }
    }
}
