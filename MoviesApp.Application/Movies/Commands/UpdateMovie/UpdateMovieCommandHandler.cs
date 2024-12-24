using Mapster;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using MoviesApp.Domain.Entities;
using MoviesApp.Domain.Interfaces;
using MoviesApp.Infrastructure.Global;

namespace MoviesApp.Application.Movies.Commands.UpdateMovie
{
    public class UpdateMovieCommandHandler : BaseHandler<UpdateMovieCommandHandler>, IRequestHandler<UpdateMovieCommand, Movie>
    {
        private readonly IMoviesRepository _moviesRepository;
        public UpdateMovieCommandHandler(IMoviesRepository moviesRepository, 
            IMemoryCache memoryCache, 
            ILogger<UpdateMovieCommandHandler> logger) : base(memoryCache, logger)
        {
            _moviesRepository = moviesRepository ?? throw new ArgumentNullException(nameof(moviesRepository));
        }

        public async Task<Movie> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"UpdateMovieCommand handler execute {DateTime.UtcNow}");

            var updateMovie = request.Adapt<Movie>();

            await _moviesRepository.Update(updateMovie, cancellationToken);

            _memoryCache.Remove(CacheItemKeys.allMoviesKey);
            _memoryCache.Remove($"{CacheItemKeys.movieByIdKey}_{request.Id}");

            return updateMovie;
        }
    }
}
