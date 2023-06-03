using System.Net.Http.Headers;

public class FourSquarePlacesService : IFourSquarePlacesService
{
    private readonly HttpClient _httpClient;

    public FourSquarePlacesService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClient = httpClientFactory.CreateClient($"foursquare");
    }

    public async Task<HttpResponseMessage> GetPlaces(double? latitude, double? longitude, string? fields, string? query, int? limit)
    {
        return await _httpClient.GetAsync($"/v3/places/nearby");
    }
}