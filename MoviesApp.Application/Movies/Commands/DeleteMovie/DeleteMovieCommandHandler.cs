using MediatR;
using Microsoft.Extensions.Caching.Memory;
using MoviesApp.Domain.Interfaces;
using MoviesApp.Infrastructure.Global;

namespace MoviesApp.Application.Movies.Commands.DeleteMovie
{
    public class DeleteMovieCommandHandler : IRequestHandler<DeleteMovieCommand>
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IMoviesRepository _moviesRepository;
        public DeleteMovieCommandHandler(IMoviesRepository moviesRepository, IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
            _moviesRepository = moviesRepository ?? throw new ArgumentNullException(nameof(moviesRepository));
        }

        public async Task Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
        {
            await _moviesRepository.Remove(request.MovieId, cancellationToken);

            _memoryCache.Remove(CacheItemKeys.allMoviesKey);
        }
    }
}
