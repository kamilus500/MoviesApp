using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using MoviesApp.Domain.Interfaces;
using MoviesApp.Infrastructure.Global;
using MoviesApp.Infrastructure.Repositories;

namespace MoviesApp.Application.Movies.Commands.DeleteMovie
{
    public class DeleteMovieCommandHandler : BaseHandler<DeleteMovieCommandHandler>, IRequestHandler<DeleteMovieCommand>
    {
        private readonly IMoviesRepository _moviesRepository;

        public DeleteMovieCommandHandler(IMoviesRepository moviesRepository, 
            IMemoryCache memoryCache,
            ILogger<DeleteMovieCommandHandler> logger) : base(memoryCache, logger)
        {
            _moviesRepository = moviesRepository ?? throw new ArgumentNullException(nameof(moviesRepository));
        }

        public async Task Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"DeteleMovieCommand handler execute {DateTime.UtcNow}");

            await _moviesRepository.Remove(request.MovieId, cancellationToken);

            _memoryCache.Remove(CacheItemKeys.allMoviesKey);
        }
    }
}
