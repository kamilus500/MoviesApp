using MediatR;
using MoviesApp.Domain.Entities;

namespace MoviesApp.Application.ExternalApi.Queries.DownloadMovies
{
    public class DownloadMoviesQuery : IRequest<IEnumerable<Movie>>
    {
    }
}
