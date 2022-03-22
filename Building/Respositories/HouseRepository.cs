using Building.Dtos;

namespace Building.Respositories
{
    public class HouseRepository : IHouseRepository
    {
        private IDictionary<Guid, HouseDto> houses = new Dictionary<Guid, HouseDto>();

        public async Task Create(HouseDto house)
        {
            houses.Add(house.Id, house);

            await Task.CompletedTask;
        }
    }
}
