using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using MoviesApp.Domain.Entities;
using MoviesApp.Domain.Interfaces;
using MoviesApp.Infrastructure.Global;

namespace MoviesApp.Application.Movies.Queries.GetMovieById
{
    public class GetMovieByIdQueryHandler : IRequestHandler<GetMovieByIdQuery, Movie>
    {
        private readonly IMoviesRepository _moviesRepository;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<GetMovieByIdQueryHandler> _logger;
        public GetMovieByIdQueryHandler(IMoviesRepository moviesRepository, IMemoryCache memoryCache, ILogger<GetMovieByIdQueryHandler> logger)
        {
            _moviesRepository = moviesRepository ?? throw new ArgumentNullException(nameof(moviesRepository));    
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Movie> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"GetMovieByIdQuery handler execute {DateTime.UtcNow}");

            string cacheKey = $"{CacheItemKeys.movieByIdKey}_{request.Id}";

            if (!_memoryCache.TryGetValue(cacheKey, out Movie movie))
            {
                movie = await _moviesRepository.GetById(request.Id, cancellationToken);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(60))
                    .SetPriority(CacheItemPriority.Normal)
                    .SetSize(1024);

                _memoryCache.Set(cacheKey, movie, cacheEntryOptions);
            }
            
            return movie;
        }
    }
}
