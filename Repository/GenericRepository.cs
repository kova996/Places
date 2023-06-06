using Places.Contracts.Repository;
using Places.Data;

namespace Places.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly RequestResponseDbContext _context;

        public GenericRepository(RequestResponseDbContext context)
        {
            _context = context;
        }
        public void Add(T entity)
        {
            _context.Add(entity);
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }
    }
}
