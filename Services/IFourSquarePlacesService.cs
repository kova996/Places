using Places.Models;

public interface IFourSquarePlacesService
{
    public Task<HttpResponseMessage> GetPlaces(FoursquareRequest foursquareRequest);
}