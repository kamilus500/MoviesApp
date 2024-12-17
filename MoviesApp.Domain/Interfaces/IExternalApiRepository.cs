using MoviesApp.Domain.Entities;

namespace MoviesApp.Domain.Interfaces
{
    public interface IExternalApiRepository
    {
        Task<IEnumerable<Movie>> Download(CancellationToken cancellationToken);
    }
}
