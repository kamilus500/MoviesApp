using Mapster;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using MoviesApp.Domain.Entities;
using MoviesApp.Domain.Interfaces;
using MoviesApp.Infrastructure.Global;

namespace MoviesApp.Application.Movies.Commands.CreateMovie
{
    public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, Movie>
    {
        private readonly IMoviesRepository _moviesRepository;
        private readonly IMemoryCache _memoryCache;
        public CreateMovieCommandHandler(IMoviesRepository moviesRepository, IMemoryCache memoryCache)
        {
            _moviesRepository = moviesRepository ?? throw new ArgumentNullException(nameof(moviesRepository));
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        public async Task<Movie> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
        {
            var newMovie = request.Adapt<Movie>();

            await _moviesRepository.Create(newMovie, cancellationToken);

            _memoryCache.Remove(CacheItemKeys.allMoviesKey);

            return newMovie;
        }
    }
}
