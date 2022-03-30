using Microsoft.AspNetCore.Mvc;
using User.Application;
using User.Middleware;
using User.Requests;
using User.Responses;

namespace User.Controllers;

[ApiController]
[ServiceFilter(typeof(LogActionFilter))]
[Route("/api/v1/[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;

    public UserController(ILogger<UserController> logger, IUserService userService)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    [HttpGet]
    public ActionResult Get(Guid id)
    {
        return new OkObjectResult(_userService.Get(id));
    }

    [HttpPost]
    public ActionResult Create(CreateAppUserRequest request)
    {
        var appUser = request.ToDto();
        _userService.Create(appUser);
        return RedirectToAction("Get", new {id = appUser.Id});
    }

    [HttpPut]
    public ActionResult Update(UpdateAppUserRequest request)
    {
        _userService.Update(request.ToDto());

        return Ok();
    }

    [HttpDelete]
    public ActionResult Delete(Guid userId)
    {
        _userService.Delete(userId);
        return Ok();
    }
}