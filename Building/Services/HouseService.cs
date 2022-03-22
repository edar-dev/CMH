using Building.Dtos;
using Building.Respositories;

namespace Building.Services
{
    public class HouseService : IHouseService
    {
        private readonly ILogger<HouseService> _logger;
        private readonly IHouseRepository _houseRepositoriy;

        public HouseService(ILogger<HouseService> logger, IHouseRepository houseRepositoriy)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _houseRepositoriy = houseRepositoriy ?? throw new ArgumentNullException(nameof(houseRepositoriy));
        }
        public async Task CreateHouse(HouseDto houseDto)
        {
            _logger.LogInformation($"{nameof(CreateHouse)}");
            try
            {
                await _houseRepositoriy.Create(houseDto);
            }
            catch (Exception)
            {
                _logger.LogError($"{nameof(CreateHouse)} error");

            }

        }

        public Task<HouseDto> GetHouse(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
