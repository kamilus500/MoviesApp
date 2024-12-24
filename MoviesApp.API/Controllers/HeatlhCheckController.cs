using MediatR;
using Microsoft.AspNetCore.Mvc;
using MoviesApp.Application.HealthCheck.Queries.CheckHeatlh;

namespace MoviesApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeatlhCheckController : ControllerBase
    {
        private readonly IMediator _mediator;
        public HeatlhCheckController(IMediator mediator)
            => _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        [HttpGet("/check")]
        public async Task<IActionResult> CheckHealth()
        {
            var result = await _mediator.Send(new CheckHealthQuery());

            return result == true ? Ok() : BadRequest();
        }
    }
}
