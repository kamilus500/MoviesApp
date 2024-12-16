using MediatR;
using MoviesApp.Domain.Entities;

namespace MoviesApp.Application.Movies.Commands.CreateMovie
{
    public class CreateMovieCommand : CreateUpdateMovieDto, IRequest<Movie>
    {
    }
}
