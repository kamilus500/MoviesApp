using Microsoft.EntityFrameworkCore;
using MoviesApp.Domain.Entities;
using MoviesApp.Domain.Interfaces;
using MoviesApp.Infrastructure.Persistance;

namespace MoviesApp.Infrastructure.Repositories
{
    public class MoviesRepository : BaseRepository, IMoviesRepository
    {
        public MoviesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task Create(Movie movie, CancellationToken cancellationToken)
        {
            await dbContext.Movies.AddAsync(movie);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Movie>> GetAll(CancellationToken cancellationToken)
            => await dbContext
                            .Movies
                            .AsNoTracking()
                            .ToListAsync(cancellationToken);

        public async Task<Movie> GetById(int id, CancellationToken cancellationToken)
            => await dbContext.Movies.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id, cancellationToken);

        public async Task Remove(int id, CancellationToken cancellationToken)
        {
            var movie = await dbContext.Movies.FirstOrDefaultAsync(m => m.Id == id, cancellationToken);

            dbContext.Movies.Remove(movie);

            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(Movie movie, CancellationToken cancellationToken)
        {
            dbContext.Update(movie);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
