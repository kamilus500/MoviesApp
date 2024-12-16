using Mapster;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using MoviesApp.Application.Movies.Commands.DeleteMovie;
using MoviesApp.Domain.Entities;
using MoviesApp.Domain.Interfaces;
using MoviesApp.Infrastructure.Global;

namespace MoviesApp.Application.Movies.Commands.UpdateMovie
{
    public class UpdateMovieCommandHandler : IRequestHandler<UpdateMovieCommand, Movie>
    {
        private readonly IMoviesRepository _moviesRepository;
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<UpdateMovieCommandHandler> _logger;
        public UpdateMovieCommandHandler(IMoviesRepository moviesRepository, IMemoryCache memoryCache, ILogger<UpdateMovieCommandHandler> logger)
        {
            _moviesRepository = moviesRepository ?? throw new ArgumentNullException(nameof(moviesRepository));
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Movie> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"UpdateMovieCommand handler execute {DateTime.UtcNow}");

            var updateMovie = request.Adapt<Movie>();

            await _moviesRepository.Update(updateMovie, cancellationToken);

            _memoryCache.Remove(CacheItemKeys.allMoviesKey);

            return updateMovie;
        }
    }
}
