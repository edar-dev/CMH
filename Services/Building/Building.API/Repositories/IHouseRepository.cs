﻿using Building.Dtos;

namespace Building.Repositories
{
    public interface IHouseRepository
    {
        Task Create(HouseDto house);

        Task Update(HouseDto house);

        Task<HouseDto> Get(Guid id);

        Task Delete(Guid id);

        Task<IEnumerable<HouseDto>> GetAll();

    }
}
