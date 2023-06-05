using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Places.Data;
using Places.Hubs;
using Places.Models;

namespace Places.Controllers;

[ApiController]
[Route("[controller]")]
public class EventsController : ControllerBase
{
    private readonly ILogger<PlacesController> _logger;
    private readonly IHubContext<EventsHub> _notificationHubContext;
    private readonly RequestResponseDbContext _dbContext;

    public EventsController(ILogger<PlacesController> logger, IHubContext<EventsHub> notificationHubContext, RequestResponseDbContext dbContext)
    {
        _logger = logger;
        _notificationHubContext = notificationHubContext;
        _dbContext = dbContext;
    }

    [HttpGet]
    public IActionResult GetEvents()
    {
        try
        {
            var result = _dbContext.RequestResponseLogs.ToList();
            Console.WriteLine(result[3].ResponseBody);

            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }

    }
}
