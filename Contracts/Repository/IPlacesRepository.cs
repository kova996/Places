namespace Places.Contracts.Repository
{
    public interface IPlacesRepository
    {
        IQueryable<RequestResponseLog> GetRequestResponseLogs();
        void AddRequestResponse(RequestResponseLog requestResponseLog);
    }
}
