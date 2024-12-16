using Mapster;
using MediatR;
using MoviesApp.Domain.Entities;
using MoviesApp.Domain.Interfaces;

namespace MoviesApp.Application.Movies.Commands.CreateMovie
{
    public class CreateMovieCommandHandler : IRequestHandler<CreateMovieCommand, Movie>
    {
        private readonly IMoviesRepository _moviesRepository;
        public CreateMovieCommandHandler(IMoviesRepository moviesRepository)
        {
            _moviesRepository = moviesRepository ?? throw new ArgumentNullException(nameof(moviesRepository));
        }

        public async Task<Movie> Handle(CreateMovieCommand request, CancellationToken cancellationToken)
        {
            var newMovie = request.Adapt<Movie>();

            await _moviesRepository.Create(newMovie, cancellationToken);

            return newMovie;
        }
    }
}
