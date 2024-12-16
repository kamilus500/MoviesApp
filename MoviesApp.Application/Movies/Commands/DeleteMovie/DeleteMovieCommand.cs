using MediatR;

namespace MoviesApp.Application.Movies.Commands.DeleteMovie
{
    public class DeleteMovieCommand : IRequest
    {
        public int MovieId { get; set; }
        public DeleteMovieCommand(int movieId)
        {
            MovieId = movieId;
        }
    }
}
