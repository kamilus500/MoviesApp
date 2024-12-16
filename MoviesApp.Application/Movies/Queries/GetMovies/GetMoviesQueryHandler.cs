using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using MoviesApp.Domain.Entities;
using MoviesApp.Domain.Interfaces;
using MoviesApp.Infrastructure.Global;

namespace MoviesApp.Application.Movies.Queries.GetAll
{
    public class GetMoviesQueryHandler : IRequestHandler<GetMoviesQuery, IEnumerable<Movie>>
    {
        private readonly IMoviesRepository _moviesRepository;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<GetMoviesQueryHandler> _logger;
        public GetMoviesQueryHandler(IMoviesRepository moviesRepository, IMemoryCache memoryCache, ILogger<GetMoviesQueryHandler> logger)
        {
            _moviesRepository = moviesRepository ?? throw new ArgumentNullException(nameof(moviesRepository));
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<Movie>> Handle(GetMoviesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetMoviesQuery handler execute {DateTime.UtcNow}");

            if (!_memoryCache.TryGetValue(CacheItemKeys.allMoviesKey, out IEnumerable<Movie> movies))
            {
                movies = await _moviesRepository.GetAll(cancellationToken);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(60))
                    .SetPriority(CacheItemPriority.Normal)
                    .SetSize(1024);

                _memoryCache.Set(CacheItemKeys.allMoviesKey, movies, cacheEntryOptions);
            }

            return await _moviesRepository.GetAll(cancellationToken);
        }
    }
}
