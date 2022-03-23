using Building.Dtos;

namespace Building.Respositories
{
    public class InMemoryHouseRepository : IHouseRepository
    {
        private IDictionary<Guid, HouseDto> houses = new Dictionary<Guid, HouseDto>();

        public async Task Create(HouseDto house)
        {
            if (!houses.ContainsKey(house.Id))
            {
                houses.Add(house.Id, house);
            }
            else
            {
                throw new Exception("house with the given id already exists");
            }

            await Task.CompletedTask;
        }

        public async Task Delete(Guid id)
        {
            if (!houses.ContainsKey(id))
            {
                throw new Exception("house with the given id already exists");
            }
            else
            {
                houses.Remove(id);
            }

            await Task.CompletedTask;
        }

        public async Task<HouseDto> Get(Guid id)
        {
            bool houseExist = houses.TryGetValue(id, out HouseDto? house);

            if (!houseExist || house is null) throw new Exception("House does not exists");

            return await Task.FromResult(house);
        }

        public async Task<IEnumerable<HouseDto>> GetAll()
        {
            return await Task.FromResult(houses.Select(x => x.Value).ToList());
        }

        public async Task Update(HouseDto house)
        {
            if (!houses.ContainsKey(house.Id))
            {
                throw new Exception("house with the given id does not exists");
            }
            else
            {
                houses[house.Id] = house;
            }

            await Task.CompletedTask;
        }
    }
}
