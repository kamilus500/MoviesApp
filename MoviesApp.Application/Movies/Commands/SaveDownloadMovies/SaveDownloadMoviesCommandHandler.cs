using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using MoviesApp.Domain.Interfaces;
using MoviesApp.Infrastructure.Global;

namespace MoviesApp.Application.Movies.Commands.SaveDownloadMovies
{
    public class SaveDownloadMoviesCommandHandler : BaseHandler<SaveDownloadMoviesCommandHandler>, IRequestHandler<SaveDownloadMoviesCommand>
    {
        private readonly IMoviesRepository _moviesRepository;
        public SaveDownloadMoviesCommandHandler(IMoviesRepository moviesRepository, 
            IMemoryCache memoryCache, 
            ILogger<SaveDownloadMoviesCommandHandler> logger) : base(memoryCache, logger)
        {
            _moviesRepository = moviesRepository ?? throw new ArgumentNullException(nameof(moviesRepository));
        }

        public async Task Handle(SaveDownloadMoviesCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"SaveDownloadMoviesCommand execute {DateTime.UtcNow}");

            var existingMovies = await _moviesRepository.GetAll(cancellationToken);

            var existingMovieTitles = existingMovies
                                        .Select(m => m.Title)
                                        .ToList();

            var moviesToSave = request.Movies
                                        .Where(movie => !existingMovieTitles.Contains(movie.Title))
                                        .ToList();

            foreach (var movie in moviesToSave)
            {
                movie.Id = 0;
                await _moviesRepository.Create(movie, cancellationToken);
            }

            _memoryCache.Remove(CacheItemKeys.allMoviesKey);
        }
    }
}
