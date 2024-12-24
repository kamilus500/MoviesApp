namespace MoviesApp.Domain.Interfaces
{
    public interface IHealthCheckRepository
    {
        Task<bool> IsHealthy(CancellationToken cancellationToken);
    }
}
