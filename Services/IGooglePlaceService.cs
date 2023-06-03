public interface IGooglePlacesService
{
    public Task<string> GetDataFromOtherApi(string url);
    public Task<string> GetPlaceData(string placeId);
}