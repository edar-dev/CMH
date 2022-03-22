using Building.Dtos;

namespace Building.Respositories
{
    public interface IHouseRepository
    {
        Task Create(HouseDto house);
    }
}
