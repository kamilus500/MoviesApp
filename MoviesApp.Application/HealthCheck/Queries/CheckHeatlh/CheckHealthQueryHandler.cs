using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using MoviesApp.Application.Movies;
using MoviesApp.Domain.Interfaces;

namespace MoviesApp.Application.HealthCheck.Queries.CheckHeatlh
{
    public class CheckHealthQueryHandler : BaseHandler<CheckHealthQueryHandler>, IRequestHandler<CheckHealthQuery, bool>
    {
        private readonly IHealthCheckRepository _healthCheckRepository;
        public CheckHealthQueryHandler(IHealthCheckRepository healthCheckRepository,
            IMemoryCache memoryCache, 
            ILogger<CheckHealthQueryHandler> logger) : base(memoryCache, logger)
        {
            _healthCheckRepository = healthCheckRepository ?? throw new ArgumentNullException(nameof(healthCheckRepository));
        }

        public async Task<bool> Handle(CheckHealthQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"CheckHealthQuery executed at {DateTime.UtcNow}");

            return await _healthCheckRepository.IsHealthy(cancellationToken);
        }
    }
}
