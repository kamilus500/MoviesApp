using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using MoviesApp.Application.Movies;
using MoviesApp.Domain.Entities;
using MoviesApp.Domain.Interfaces;

namespace MoviesApp.Application.ExternalApi.Queries.DownloadMovies
{
    public class DownloadMoviesQueryHandler : BaseHandler<DownloadMoviesQueryHandler>, IRequestHandler<DownloadMoviesQuery, IEnumerable<Movie>>
    {
        private readonly IExternalApiRepository _externalApiRepository;
        public DownloadMoviesQueryHandler(IExternalApiRepository externalApiRepository,
            IMoviesRepository moviesRepository,
            IMemoryCache memoryCache,
            ILogger<DownloadMoviesQueryHandler> logger) : base(moviesRepository, memoryCache, logger)
        {
            _externalApiRepository = externalApiRepository ?? throw new ArgumentNullException(nameof(externalApiRepository));
        }

        public async Task<IEnumerable<Movie>> Handle(DownloadMoviesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"DownloadMoviesQuery execute {DateTime.UtcNow}");

            return await _externalApiRepository.Download(cancellationToken);
        }
    }
}
