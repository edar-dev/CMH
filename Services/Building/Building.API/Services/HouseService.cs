using Building.Dtos;
using Building.Repositories;
using Building.Validators;
using FluentValidation;

namespace Building.Services;

public class HouseService : IHouseService
{
    private readonly ILogger<HouseService> _logger;
    private readonly IHouseRepository _houseRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IValidator<HouseDto> _houseDtoValidator;
    private readonly IValidator<IEnumerable<RoomDto>> _roomsDtoValidator;

    public HouseService(ILogger<HouseService> logger, IHouseRepository houseRepository,
        IRoomRepository roomRepository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _houseRepository = houseRepository ?? throw new ArgumentNullException(nameof(houseRepository));
        _roomRepository = roomRepository ?? throw new ArgumentNullException(nameof(roomRepository));
        _houseDtoValidator = new HouseDtoValidator();
        _roomsDtoValidator = new RoomsDtoValidator();
    }

    public async Task CreateHouse(HouseDto houseDto, IEnumerable<RoomDto> rooms)
    {
        _logger.LogInformation($"{nameof(CreateHouse)}");
        await _houseDtoValidator.ValidateAndThrowAsync(houseDto);
        await _roomsDtoValidator.ValidateAndThrowAsync(rooms);

        ValidateHouseWithRooms(houseDto, rooms);
        try
        {
            await _houseRepository.Create(houseDto);

            await _roomRepository.Create(rooms);
        }
        catch (Exception)
        {
            _logger.LogError($"{nameof(CreateHouse)} error");
        }
    }

    private static void ValidateHouseWithRooms(HouseDto houseDto, IEnumerable<RoomDto> rooms)
    {
        // all the rooms must have the same houseId of the house
        if (rooms.Any(r => r.HouseId != houseDto.Id))
            throw new ValidationException("Rooms must have the correct houseId");
    }

    public async Task<FullHouseDto> GetHouse(Guid id)
    {
        _logger.LogInformation($"{nameof(GetHouse)}");
        try
        {
            var houseDto = await _houseRepository.Get(id);
            var rooms = await _roomRepository.GetByHouseId(id);

            return new FullHouseDto(houseDto, rooms);
        }
        catch (Exception)
        {
            _logger.LogError($"{nameof(GetHouse)} error");
            throw;
        }
    }

    public async Task UpdateHouse(HouseDto house)
    {
        _logger.LogInformation($"{nameof(UpdateHouse)}");
        try
        {
            await _houseRepository.Update(house);
        }
        catch (Exception)
        {
            _logger.LogError($"{nameof(UpdateHouse)} error");
            throw;
        }
    }

    public async Task AddRoom(RoomDto roomDto)
    {
        _logger.LogInformation($"{nameof(AddRoom)}");
        try
        {
            await _roomRepository.Create(roomDto);
        }
        catch (Exception)
        {
            _logger.LogError($"{nameof(AddRoom)} error");
            throw;
        }
    }

    public async Task UpdateRoom(RoomDto roomDto)
    {
        _logger.LogInformation($"{nameof(UpdateRoom)}");
        try
        {
            await _roomRepository.Update(roomDto);
        }
        catch (Exception)
        {
            _logger.LogError($"{nameof(UpdateRoom)} error");
            throw;
        }
    }
}