using Places.Contracts.Repository;
using Places.Data;

namespace Places.Repository
{
    public class PlacesRepository : GenericRepository<RequestResponseLog>, IPlacesRepository
    {
        private readonly RequestResponseDbContext _context;

        public PlacesRepository(RequestResponseDbContext context) : base(context)
        {
            _context = context;
        }
        public void AddRequestResponse(RequestResponseLog requestResponseLog)
        {
            Add(requestResponseLog);
            _context.SaveChanges();
        }

        public IQueryable<RequestResponseLog> GetRequestResponseLogs()
        {
            return GetAll();
        }
    }
}
