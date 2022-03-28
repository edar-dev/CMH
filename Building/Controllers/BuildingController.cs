using Building.Requests;
using Building.Services;
using Microsoft.AspNetCore.Mvc;

namespace Building.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class BuildingController : ControllerBase
{
    private readonly ILogger<BuildingController> _logger;
    private readonly IHouseService _houseService;

    public BuildingController(ILogger<BuildingController> logger, IHouseService houseService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _houseService = houseService ?? throw new ArgumentNullException(nameof(houseService));
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreateHouseRequest createHouseRequest)
    {
        _logger.LogInformation("");

        var houseId = Guid.NewGuid();

        try
        {
            await _houseService.CreateHouse(createHouseRequest.ToDto(houseId),
                createHouseRequest.Rooms.Select(r => r.ToDto(Guid.NewGuid(), houseId)));
        }
        catch (Exception)
        {
            return BadRequest();
        }

        return RedirectToAction("Get", new {id = houseId});
    }

    [HttpPut]
    public async Task<ActionResult> UpdateHouse(UpdateHouseRequest request)
    {
        _logger.LogInformation("");

        try
        {
            await _houseService.UpdateHouse(request.ToDto());
            return Ok();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return BadRequest();
        }
    }

    [HttpPost]
    public async Task<ActionResult> AddRoom(AddRoomRequest request)
    {
        _logger.LogInformation("");

        try
        {
            await _houseService.AddRoom(request.ToRoomDto(Guid.NewGuid()));
            return Ok();
        }
        catch (Exception)
        {
            return BadRequest();
        }

    }
    
    [HttpPost]
    public async Task<ActionResult> UpdateRoom(UpdateRoomRequest request)
    {
        _logger.LogInformation("");

        try
        {
            await _houseService.UpdateRoom(request.ToDto());
            return Ok();
        }
        catch (Exception)
        {
            return BadRequest();
        }

    }

    [HttpGet]
    public async Task<ActionResult> Get(Guid id)
    {
        _logger.LogInformation("");

        try
        {
            var house = await _houseService.GetHouse(id);
            return Ok(house);
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
}