using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoviesApp.Domain.Interfaces;
using MoviesApp.Infrastructure.Persistance;
using MoviesApp.Infrastructure.Repositories;
using Serilog;

namespace MoviesApp.Infrastructure.Extensions
{
    public static class ServiceCollecionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration, ConfigureHostBuilder host)
        {
            var connectionString = configuration.GetConnectionString("connectionString") ?? throw new ArgumentNullException("Connection string is empty");

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            host.UseSerilog();

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            services.AddCors(options =>
            {
                options.AddPolicy("DevPolicy", builder =>
                {
                    builder.WithOrigins("http://localhost:8080")
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            services.AddMemoryCache();

            services.AddTransient<IMoviesRepository, MoviesRepository>();
        }
    }
}
