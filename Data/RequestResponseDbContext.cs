using Microsoft.EntityFrameworkCore;

namespace Places.Data
{
    public class RequestResponseDbContext : DbContext
    {
        public DbSet<RequestResponseLog> RequestResponseLogs { get; set; }

        public RequestResponseDbContext(DbContextOptions<RequestResponseDbContext> options) : base(options)
        {
        }
    }
}
