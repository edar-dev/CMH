using Building.Dtos;

namespace Building.Services
{
    public interface IHouseService
    {
        Task CreateHouse(HouseDto houseDto);
        Task<HouseDto> GetHouse(Guid id);
    }
}
