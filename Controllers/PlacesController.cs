using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Places.Data;
using Places.Hubs;
using Places.Models;

namespace Places.Controllers;

[ApiController]
[Route("[controller]")]
public class PlacesController : ControllerBase
{
    private readonly ILogger<PlacesController> _logger;
    private readonly IFourSquarePlacesService _fourSquarePlacesService;
    private readonly IHubContext<EventsHub> _notificationHubContext;
    private readonly RequestResponseDbContext _dbContext;

    public PlacesController(ILogger<PlacesController> logger, IFourSquarePlacesService fourSquarePlacesService, IHubContext<EventsHub> notificationHubContext, RequestResponseDbContext dbContext)
    {
        _logger = logger;
        _fourSquarePlacesService = fourSquarePlacesService;
        _notificationHubContext = notificationHubContext;
        _dbContext = dbContext;
    }

    [HttpGet("List")]
    public async Task<IActionResult> GetNearbyPlaces()
    {
        var response = _fourSquarePlacesService.GetPlaces(new FoursquareRequest()).Result;
        var result = await response.Content.ReadFromJsonAsync<FoursquareResponse>();

        if (response.IsSuccessStatusCode)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest(result);
        }
    }

    [HttpPost("List")]
    public async Task<IActionResult> GetNearbyPlacesFiltered([FromBody] FoursquareRequest request)
    {
        string a = request.GetQueryString();
        Console.WriteLine(a);
        string requestUrl = $"{Request.Scheme}://{Request.Host}{Request.Path}{Request.QueryString}";

        var response = _fourSquarePlacesService.GetPlaces(request).Result;
        FoursquareResponse? result = await response.Content.ReadFromJsonAsync<FoursquareResponse>();

        result.results.ToList().ForEach(el => Console.WriteLine(el.name));

        await _notificationHubContext.Clients.All.SendAsync("ReceiveMessage", request);

        if (response.IsSuccessStatusCode)
        {
            var log = new RequestResponseLog
            {
                RequestUrl = requestUrl,
                RequestBody = JsonSerializer.Serialize(request),
                ResponseStatusCode = (int)response.StatusCode,
                ResponseBody =  JsonSerializer.Serialize(result),
                LogDateTime = DateTime.Now
            };
            _dbContext.RequestResponseLogs.Add(log);
            await _dbContext.SaveChangesAsync();
            return Ok(result);
        }
        else
        {
            return BadRequest(result);
        }
        // TODO: SAVE THE RIGHT CONTENT
    }
}
