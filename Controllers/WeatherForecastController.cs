using Microsoft.AspNetCore.Mvc;

namespace Places.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IGooglePlacesService _service;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IGooglePlacesService googlePlacesService)
    {
        _logger = logger;
        _service = googlePlacesService;
    }

    [HttpGet("basic")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpGet("test")]
    public string GetTest()
    {
        return _service.GetPlaceData("a").Result;
    }
}
