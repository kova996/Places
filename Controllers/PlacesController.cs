using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Places.Contracts.Repository;
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
    private readonly IRepositoryManager _repo;

    public PlacesController(ILogger<PlacesController> logger, IFourSquarePlacesService fourSquarePlacesService, IHubContext<EventsHub> notificationHubContext,
        IRepositoryManager repo)
    {
        _logger = logger;
        _fourSquarePlacesService = fourSquarePlacesService;
        _notificationHubContext = notificationHubContext;
        _repo = repo;
    }

    [HttpGet("List")]
    public async Task<IActionResult> GetNearbyPlaces()
    {
        var response = await _fourSquarePlacesService.GetPlaces(new FoursquareRequest());
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

        var response = await _fourSquarePlacesService.GetPlaces(request);
        FoursquareResponse? result = await response.Content.ReadFromJsonAsync<FoursquareResponse>();

        result?.results.ToList().ForEach(el => Console.WriteLine(el.name));

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
            _repo.PlacesRepository.AddRequestResponse(log);
            //await _repo.Save();
            return Ok(result);
        }
        else
        {
            return BadRequest(result);
        }
        // TODO: SAVE THE RIGHT CONTENT
    }
}
