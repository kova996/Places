using System.Collections.Specialized;
using Microsoft.AspNetCore.Mvc;

namespace Places.Controllers;

[ApiController]
[Route("[controller]")]
public class PlacesController : ControllerBase
{
    private readonly ILogger<PlacesController> _logger;
    private readonly IFourSquarePlacesService _fourSquarePlacesService;

    public PlacesController(ILogger<PlacesController> logger, IFourSquarePlacesService fourSquarePlacesService)
    {
        _logger = logger;
        _fourSquarePlacesService = fourSquarePlacesService;
    }

    [HttpGet("List")]
    public async Task<IActionResult> GetNearbyPlaces()
    {
        var response = _fourSquarePlacesService.GetPlaces(null, null, null, null, null).Result;
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
        var options = new NameValueCollection
            {
                { "ll", $"{request.latitude},{request.longitude}" },
                { "client_id", request.clientId },
                { "client_secret", request.clientSecret },
                { "v", "20211001" },
                { "query", request.query },
                { "limit", request.limit.ToString() }
            };
        //v3/places/search?
        //var response = await _fourSquarePlacesService.GetAsync($"/v2/venues/search?{options.ToQueryString()}");
        var response = _fourSquarePlacesService.GetPlaces(null, null, null, null, null).Result;
        FoursquareResponse? result = await response.Content.ReadFromJsonAsync<FoursquareResponse>();

        if (response.IsSuccessStatusCode)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest(result);
        }
    }
}
