using Mapster;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using MoviesApp.Domain.Entities;
using MoviesApp.Domain.Interfaces;
using MoviesApp.Infrastructure.Global;

namespace MoviesApp.Application.Movies.Commands.UpdateMovie
{
    public class UpdateMovieCommandHandler : IRequestHandler<UpdateMovieCommand, Movie>
    {
        private readonly IMoviesRepository _moviesRepository;
        private readonly IMemoryCache _memoryCache;
        public UpdateMovieCommandHandler(IMoviesRepository moviesRepository, IMemoryCache memoryCache)
        {
            _moviesRepository = moviesRepository ?? throw new ArgumentNullException(nameof(moviesRepository));
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        public async Task<Movie> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
        {
            var updateMovie = request.Adapt<Movie>();

            await _moviesRepository.Update(updateMovie, cancellationToken);

            _memoryCache.Remove(CacheItemKeys.allMoviesKey);

            return updateMovie;
        }
    }
}
