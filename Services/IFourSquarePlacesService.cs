public interface IFourSquarePlacesService
{
    public Task<HttpResponseMessage> GetPlaces(double? latitude, double? longitude, string? fields, string? query, int? limit);
}