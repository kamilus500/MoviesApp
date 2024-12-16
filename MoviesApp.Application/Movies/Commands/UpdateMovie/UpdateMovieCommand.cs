using MediatR;
using MoviesApp.Domain.Entities;

namespace MoviesApp.Application.Movies.Commands.UpdateMovie
{
    public class UpdateMovieCommand : CreateUpdateMovieDto, IRequest<Movie>
    {
    }
}
