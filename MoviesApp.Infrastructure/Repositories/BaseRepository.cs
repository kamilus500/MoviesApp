using MoviesApp.Infrastructure.Persistance;

namespace MoviesApp.Infrastructure.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly ApplicationDbContext dbContext;
        public BaseRepository(ApplicationDbContext dbContext)
            => this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }
}
