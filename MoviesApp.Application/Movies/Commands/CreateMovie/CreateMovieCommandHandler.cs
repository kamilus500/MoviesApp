using Mapster;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using MoviesApp.Domain.Entities;
using MoviesApp.Domain.Interfaces;
using MoviesApp.Infrastructure.Global;

namespace MoviesApp.Application.Movies.Commands.CreateMovie
{
    public class CreateMovieCommandHandler : BaseHandler<CreateMovieCommandHandler>, IRequestHandler<CreateMovieCommand, Movie>
    {
        private readonly IMoviesRepository _moviesRepository;
        public CreateMovieCommandHandler(IMoviesRepository moviesRepository, 
            IMemoryCache memoryCache, 
            ILogger<CreateMovieCommandHandler> logger) : base(memoryCache, logger)
        {
            _moviesRepository = moviesRepository ?? throw new ArgumentNullException(nameof(moviesRepository));
        }

        public async Task<Movie> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CreateMovieCommand handler execute {DateTime.UtcNow}");

            var newMovie = request.Adapt<Movie>();

            await _moviesRepository.Create(newMovie, cancellationToken);

            _memoryCache.Remove(CacheItemKeys.allMoviesKey);

            return newMovie;
        }
    }
}
