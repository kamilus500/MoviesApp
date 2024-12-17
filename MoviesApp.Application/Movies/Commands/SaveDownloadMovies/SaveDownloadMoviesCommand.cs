using MediatR;
using MoviesApp.Domain.Entities;

namespace MoviesApp.Application.Movies.Commands.SaveDownloadMovies
{
    public class SaveDownloadMoviesCommand : IRequest
    {
        public IEnumerable<Movie> Movies { get; set; }

        public SaveDownloadMoviesCommand(IEnumerable<Movie> movies)
        {
            Movies = movies;
        }
    }
}
