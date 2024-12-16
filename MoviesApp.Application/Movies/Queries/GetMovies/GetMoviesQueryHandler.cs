using MediatR;
using MoviesApp.Domain.Entities;
using MoviesApp.Domain.Interfaces;

namespace MoviesApp.Application.Movies.Queries.GetAll
{
    public class GetMoviesQueryHandler : IRequestHandler<GetMoviesQuery, IEnumerable<Movie>>
    {
        private readonly IMoviesRepository _moviesRepository;
        public GetMoviesQueryHandler(IMoviesRepository moviesRepository)
            => _moviesRepository = moviesRepository ?? throw new ArgumentNullException(nameof(moviesRepository));

        public async Task<IEnumerable<Movie>> Handle(GetMoviesQuery request, CancellationToken cancellationToken)
        {
            return await _moviesRepository.GetAll(cancellationToken);
        }
    }
}
