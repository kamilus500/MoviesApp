using MoviesApp.Domain.Entities;
using MoviesApp.Domain.Interfaces;
using Newtonsoft.Json;

namespace MoviesApp.Infrastructure.Repositories
{
    public class ExternalApiRepository : IExternalApiRepository
    {
        private readonly HttpClient _httpClient;
        public ExternalApiRepository(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<IEnumerable<Movie>> Download(CancellationToken cancellationToken)
        {
            string apiUrl = "https://filmy.programdemo.pl/MyMovies";

            var response = await _httpClient.GetAsync(apiUrl, cancellationToken);

            string jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken);

            return JsonConvert.DeserializeObject<List<Movie>>(jsonResponse);
        }
    }
}
