using MediatR;
using MoviesApp.Domain.Entities;

namespace MoviesApp.Application.Movies.Queries.GetAll
{
    public class GetMoviesQuery : IRequest<IEnumerable<Movie>>
    {

    }
}
