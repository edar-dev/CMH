using Building.Dtos;

namespace Building.Services;

public interface IHouseService
{
    Task CreateHouse(HouseDto houseDto, IEnumerable<RoomDto> rooms);
    Task<FullHouseDto> GetHouse(Guid id);
    Task UpdateHouse(HouseDto toDto);
    Task AddRoom(RoomDto roomDto);
    Task UpdateRoom(RoomDto roomDto);
}