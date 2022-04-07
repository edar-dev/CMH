using Building.Dtos;

namespace Building.Repositories
{
    public class InMemoryHouseRepository : IHouseRepository
    {
        private readonly IDictionary<Guid, HouseDto> _houses = new Dictionary<Guid, HouseDto>();

        public async Task Create(HouseDto house)
        {
            if (!_houses.ContainsKey(house.Id))
            {
                _houses.Add(house.Id, house);
            }
            else
            {
                throw new Exception("house with the given id already exists");
            }

            await Task.CompletedTask;
        }

        public async Task Delete(Guid id)
        {
            if (!_houses.ContainsKey(id))
            {
                throw new Exception("house with the given id already exists");
            }
            else
            {
                _houses.Remove(id);
            }

            await Task.CompletedTask;
        }

        public async Task<HouseDto> Get(Guid id)
        {
            bool houseExist = _houses.TryGetValue(id, out HouseDto? house);

            if (!houseExist || house is null) throw new Exception("House does not exists");

            return await Task.FromResult(house);
        }

        public async Task<IEnumerable<HouseDto>> GetAll()
        {
            return await Task.FromResult(_houses.Select(x => x.Value).ToList());
        }

        public async Task Update(HouseDto house)
        {
            if (!_houses.ContainsKey(house.Id))
            {
                throw new Exception("house with the given id does not exists");
            }
            else
            {
                _houses[house.Id] = house;
            }

            await Task.CompletedTask;
        }
    }
}