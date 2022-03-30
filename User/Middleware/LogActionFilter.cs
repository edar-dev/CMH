using Microsoft.AspNetCore.Mvc.Filters;

namespace User.Middleware;

public class LogActionFilter : ActionFilterAttribute

{
    private readonly ILogger<LogActionFilter> _logger;

    public LogActionFilter(ILogger<LogActionFilter> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        Log("OnActionExecuting", filterContext.RouteData);
    }

    public override void OnActionExecuted(ActionExecutedContext filterContext)
    {
        Log("OnActionExecuted", filterContext.RouteData);
    }

    public override void OnResultExecuting(ResultExecutingContext filterContext)
    {
        Log("OnResultExecuting", filterContext.RouteData);
    }

    public override void OnResultExecuted(ResultExecutedContext filterContext)
    {
        Log("OnResultExecuted", filterContext.RouteData);
    }

    private void Log(string methodName, RouteData routeData)
    {
        var controllerName = routeData.Values["controller"];
        var actionName = routeData.Values["action"];
        _logger.LogInformation("Method {methodName}: Controller: {controllerName} -> Action {actionName}", methodName,
            controllerName, actionName);
    }

    private void Errorog(string methodName, RouteData routeData)
    {
        var controllerName = routeData.Values["controller"];
        var actionName = routeData.Values["action"];
        var message = String.Format("{0} controller:{1} action:{2}", methodName, controllerName, actionName);
        _logger.LogError("Method {methodName}: Controller: {controllerName} -> Action {actionName}", methodName,
            controllerName, actionName);
    }
}