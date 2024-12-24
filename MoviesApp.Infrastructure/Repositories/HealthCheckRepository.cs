using MoviesApp.Domain.Interfaces;
using MoviesApp.Infrastructure.Persistance;

namespace MoviesApp.Infrastructure.Repositories
{
    public class HealthCheckRepository : BaseRepository, IHealthCheckRepository
    {
        public HealthCheckRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> IsHealthy(CancellationToken cancellationToken)
            => await dbContext.Database.CanConnectAsync(cancellationToken).ConfigureAwait(false);
    }
}
