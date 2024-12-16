using MediatR;
using MoviesApp.Domain.Entities;

namespace MoviesApp.Application.Movies.Queries.GetMovieById
{
    public class GetMovieByIdQuery : IRequest<Movie>
    {
        public int Id { get; set; }
        public GetMovieByIdQuery(int id)
        {
            Id = id;
        }
    }
}
