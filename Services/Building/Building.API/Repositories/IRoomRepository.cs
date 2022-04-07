using Building.Dtos;

namespace Building.Repositories;

public interface IRoomRepository
{
    Task Create(RoomDto room);

    Task Create(IEnumerable<RoomDto> roomsToCreate);

    Task Update(RoomDto room);

    Task Update(IEnumerable<RoomDto> roomsToUpdate);

    Task<RoomDto> Get(Guid id);

    Task Delete(Guid id);

    Task<IEnumerable<RoomDto>> GetAll();

    Task<IEnumerable<RoomDto>> GetByHouseId(Guid houseId);
}