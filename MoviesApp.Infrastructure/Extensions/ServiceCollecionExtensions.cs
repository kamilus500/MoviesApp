using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoviesApp.Domain.Interfaces;
using MoviesApp.Infrastructure.Persistance;
using MoviesApp.Infrastructure.Repositories;

namespace MoviesApp.Infrastructure.Extensions
{
    public static class ServiceCollecionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("connectionString") ?? throw new ArgumentNullException("Connection string is empty");

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            services.AddTransient<IMoviesRepository, MoviesRepository>();
        }
    }
}
