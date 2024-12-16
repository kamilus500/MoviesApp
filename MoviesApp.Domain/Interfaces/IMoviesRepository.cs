using MoviesApp.Domain.Entities;

namespace MoviesApp.Domain.Interfaces
{
    public interface IMoviesRepository
    {
        Task<IEnumerable<Movie>> GetAll(CancellationToken cancellationToken);

        Task<Movie> GetById(int id, CancellationToken cancellationToken);

        Task Create(Movie movie, CancellationToken cancellationToken);

        Task Remove(int id, CancellationToken cancellationToken);

        Task Update(Movie movie, CancellationToken cancellationToken);
    }
}
