using MediatR;
using MoviesApp.Domain.Entities;
using MoviesApp.Domain.Interfaces;

namespace MoviesApp.Application.Movies.Queries.GetMovieById
{
    public class GetMovieByIdQueryHandler : IRequestHandler<GetMovieByIdQuery, Movie>
    {
        private readonly IMoviesRepository _moviesRepository;
        public GetMovieByIdQueryHandler(IMoviesRepository moviesRepository)
        {
            _moviesRepository = moviesRepository ?? throw new ArgumentNullException(nameof(moviesRepository));    
        }

        public async Task<Movie> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
        {
            return await _moviesRepository.GetById(request.Id, cancellationToken);
        }
    }
}
