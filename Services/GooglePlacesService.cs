using System.Net.Http.Headers;

public class GooglePlacesService : IGooglePlacesService
{
    private readonly HttpClient _httpClient;
    private readonly string? _apiKey;

    public GooglePlacesService(HttpClient httpClient, IConfiguration configuration)
    {
        // TODO: add authorization header dinamically
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization","fsq3hkxURRfiv7B/KPNReX6aOLiEkK/NnRZD2YocVwTwsPI=");
        _apiKey = configuration["GooglePlacesApiKey"];
    }

    public async Task<string> GetDataFromOtherApi(string url)
    {
        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
     public async Task<string> GetPlaceData(string placeId)
    {
        var response = await _httpClient.GetAsync($"https://api.foursquare.com/v3/places/nearby");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}
// foursquare key = fsq3hkxURRfiv7B/KPNReX6aOLiEkK/NnRZD2YocVwTwsPI=