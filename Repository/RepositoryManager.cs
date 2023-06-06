using Places.Contracts.Repository;
using Places.Data;

namespace Places.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RequestResponseDbContext _context;
        private IPlacesRepository _placesRepository;

        public RepositoryManager(RequestResponseDbContext context) {
            _context = context;
        }
        public IPlacesRepository PlacesRepository
        {
            get
            {
                if (_placesRepository == null)
                {
                    _placesRepository = new PlacesRepository(_context);
                }
                return _placesRepository;
            }
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }


    }
}
