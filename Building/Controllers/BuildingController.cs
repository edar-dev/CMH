using Building.Models;
using Building.Services;
using Microsoft.AspNetCore.Mvc;

namespace Building.Controllers
{
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
        public async Task<ActionResult> Post([FromBody] CreateHouseModel createHouseRequest)
        {
            _logger.LogInformation("");

            Guid houseId = Guid.NewGuid();

            try
            {
               await _houseService.CreateHouse(createHouseRequest.ToDto(houseId));
            }
            catch (Exception)
            {
                return BadRequest();
            }

            return RedirectToAction("Get", new { id = houseId });
        }

        [HttpGet]
        public async Task<ActionResult> Get(Guid id)
        {
            _logger.LogInformation("");

            Guid houseId = Guid.NewGuid();

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
}