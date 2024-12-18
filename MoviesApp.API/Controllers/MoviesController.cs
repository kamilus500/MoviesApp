using MediatR;
using Microsoft.AspNetCore.Mvc;
using MoviesApp.Application.ExternalApi.Queries.DownloadMovies;
using MoviesApp.Application.Movies.Commands.CreateMovie;
using MoviesApp.Application.Movies.Commands.DeleteMovie;
using MoviesApp.Application.Movies.Commands.SaveDownloadMovies;
using MoviesApp.Application.Movies.Commands.UpdateMovie;
using MoviesApp.Application.Movies.Queries.GetAll;
using MoviesApp.Application.Movies.Queries.GetMovieById;

namespace MoviesApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public MoviesController(IMediator mediator)
            => _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        [HttpGet("/Movies")]
        public async Task<ActionResult> GetMovies()
            => Ok(await _mediator.Send(new GetMoviesQuery()));

        [HttpGet("/Movie/{id}")]
        public async Task<ActionResult> GetMovieById([FromRoute] int id)
        {
            var movie = await _mediator.Send(new GetMovieByIdQuery(id));

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }

        [HttpPost("/Create")]
        public async Task<ActionResult> CreateMovie([FromBody] CreateMovieCommand command)
        {
            var newMovieId = await _mediator.Send(command);

            return Created($"/CreateMovie/{newMovieId}", newMovieId);
        }

        [HttpPut("/Update")]
        public async Task<ActionResult> UpdateMovie([FromBody] UpdateMovieCommand command)
            => Ok(await _mediator.Send(command));

        [HttpDelete("/Remove/{id}")]
        public async Task<ActionResult> RemoveMovie([FromRoute] int id)
        {
            await _mediator.Send(new DeleteMovieCommand(id));
            return NoContent();
        }

        [HttpGet("/DownloadAndSave")]
        public async Task<ActionResult> DownloadAndSave()
        {
            var newMovies = await _mediator.Send(new DownloadMoviesQuery());

            await _mediator.Send(new SaveDownloadMoviesCommand(newMovies));

            return NoContent();
        }
    }
}
