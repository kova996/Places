using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Places.Contracts.Repository;
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
    private readonly IRepositoryManager _repo;

    public EventsController(ILogger<PlacesController> logger, IHubContext<EventsHub> notificationHubContext, IRepositoryManager repo)
    {
        _logger = logger;
        _notificationHubContext = notificationHubContext;
        _repo = repo;
    }

    [HttpGet]
    public IActionResult GetEvents()
    {
        try
        {
            var result = _repo.PlacesRepository.GetRequestResponseLogs().ToList();
            if (result is null)
                return NotFound();
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }

    }
}
