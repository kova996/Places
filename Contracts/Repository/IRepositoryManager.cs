namespace Places.Contracts.Repository
{
    public interface IRepositoryManager
    {
        IPlacesRepository PlacesRepository { get; }
        Task Save();
    }
}
