using Building.Dtos;
using Building.Respositories;

namespace Building.Services
{
    public class HouseService : IHouseService
    {
        private readonly ILogger<HouseService> _logger;
        private readonly IHouseRepository _houseRepository;

        public HouseService(ILogger<HouseService> logger, IHouseRepository houseRepositoriy)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _houseRepository = houseRepositoriy ?? throw new ArgumentNullException(nameof(houseRepositoriy));
        }
        public async Task CreateHouse(HouseDto houseDto)
        {
            _logger.LogInformation($"{nameof(CreateHouse)}");
            try
            {
                await _houseRepository.Create(houseDto);
            }
            catch (Exception)
            {
                _logger.LogError($"{nameof(CreateHouse)} error");

            }

        }

        public async Task<HouseDto> GetHouse(Guid id)
        {
            _logger.LogInformation($"{nameof(GetHouse)}");
            try
            {
                return await _houseRepository.Get(id);
            }
            catch (Exception)
            {
                _logger.LogError($"{nameof(GetHouse)} error");
                throw;
            }
        }
    }
}
