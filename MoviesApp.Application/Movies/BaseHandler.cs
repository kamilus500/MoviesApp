using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using MoviesApp.Domain.Interfaces;

namespace MoviesApp.Application.Movies
{
    public abstract class BaseHandler<T>
    {
        protected readonly IMemoryCache _memoryCache;
        protected readonly IMoviesRepository _moviesRepository;
        protected readonly ILogger<T> _logger;
        public BaseHandler(IMoviesRepository moviesRepository, IMemoryCache memoryCache, ILogger<T> logger)
        {
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            _moviesRepository = moviesRepository ?? throw new ArgumentNullException(nameof(moviesRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
    }
}
