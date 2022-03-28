using Building.Dtos;

namespace Building.Repositories
{
    public class InMemoryRoomRepository : IRoomRepository
    {
        private IDictionary<Guid, RoomDto> rooms = new Dictionary<Guid, RoomDto>();

        public async Task Create(RoomDto room)
        {
            if (!rooms.ContainsKey(room.Id))
            {
                rooms.Add(room.Id, room);
            }
            else
            {
                throw new Exception("room with the given id already exists");
            }

            await Task.CompletedTask;
        }

        public async Task Create(IEnumerable<RoomDto> roomsToCreate)
        {
            foreach (var room in roomsToCreate)
            {
                await Create(room);
            }
        }

        public async Task Delete(Guid id)
        {
            if (!rooms.ContainsKey(id))
            {
                throw new Exception("room with the given id already exists");
            }

            rooms.Remove(id);

            await Task.CompletedTask;
        }

        public async Task Update(IEnumerable<RoomDto> roomsToUpdate)
        {
            foreach (var room in roomsToUpdate)
            {
                await Update(room);
            }
        }

        public async Task<RoomDto> Get(Guid id)
        {
            bool roomExist = rooms.TryGetValue(id, out RoomDto? roomDto);

            if (!roomExist || roomDto is null) throw new Exception("Room does not exists");

            return await Task.FromResult(roomDto);
        }

        public async Task<IEnumerable<RoomDto>> GetAll()
        {
            return await Task.FromResult(rooms.Select(x => x.Value).ToList());
        }

        public async Task<IEnumerable<RoomDto>> GetByHouseId(Guid houseId)
        {
            return await Task.FromResult(rooms.Select(x => x.Value).Where(r => r.HouseId == houseId).ToList());
        }

        public async Task Update(RoomDto room)
        {
            if (!rooms.ContainsKey(room.Id))
            {
                throw new Exception("room with the given id does not exists");
            }

            rooms[room.Id] = room;

            await Task.CompletedTask;
        }
    }
}