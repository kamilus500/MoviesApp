using Mapster;
using MediatR;
using MoviesApp.Domain.Entities;
using MoviesApp.Domain.Interfaces;

namespace MoviesApp.Application.Movies.Commands.UpdateMovie
{
    public class UpdateMovieCommandHandler : IRequestHandler<UpdateMovieCommand, Movie>
    {
        private readonly IMoviesRepository _moviesRepository;
        public UpdateMovieCommandHandler(IMoviesRepository moviesRepository)
        {
            _moviesRepository = moviesRepository ?? throw new ArgumentNullException(nameof(moviesRepository)); 
        }

        public async Task<Movie> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
        {
            var updateMovie = request.Adapt<Movie>();

            await _moviesRepository.Update(updateMovie, cancellationToken);

            return updateMovie;
        }
    }
}
