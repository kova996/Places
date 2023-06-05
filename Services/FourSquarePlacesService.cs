using System.Net.Http.Headers;
using Places.Models;

public class FourSquarePlacesService : IFourSquarePlacesService
{
    private readonly HttpClient _httpClient;

    public FourSquarePlacesService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClient = httpClientFactory.CreateClient($"foursquare");
    }

    public async Task<HttpResponseMessage> GetPlaces(FoursquareRequest foursquareRequest)
    {
        return await _httpClient.GetAsync($"/v3/places/search?{foursquareRequest.GetQueryString()}");
    }
}