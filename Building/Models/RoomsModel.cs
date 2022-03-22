using Building.Dtos;

namespace Building.Models
{
    public record RoomsModel(string Name, double Dimension)
    {
        public RoomDto ToDto()
        {
            return new RoomDto(Name, Dimension);
        }
    }
}
