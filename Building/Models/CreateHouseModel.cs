using Building.Dtos;

namespace Building.Models
{
    public record CreateHouseModel(string Name, RoomsModel[] Rooms)
    {
        public HouseDto ToDto(Guid id)
        {
            return new HouseDto(id, Name, Rooms.Select(r => r.ToDto()));
        }
    }
}
