using MovieDirectorWebClient.Models;
using System.Text.Json;

namespace MovieDirectorWebClient.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MovieAPI");
        }

        public async Task<List<MovieDTO>> GetMoviesAsync() =>
            await _httpClient.GetFromJsonAsync<List<MovieDTO>>("Movies");

        public async Task<MovieDTO?> GetMovieAsync(int id) =>
            await _httpClient.GetFromJsonAsync<MovieDTO>($"Movies/{id}");

        public async Task<HttpResponseMessage> CreateMovieAsync(MovieDTO movie)
        {
            var json = JsonSerializer.Serialize(movie);
            Console.WriteLine("Sending JSON payload: " + json);
            return await _httpClient.PostAsJsonAsync("Movies", movie);
        }


        public async Task<HttpResponseMessage> UpdateMovieAsync(MovieDTO movie) =>
            await _httpClient.PutAsJsonAsync($"Movies/{movie.MovieId}", movie);

        public async Task<HttpResponseMessage> DeleteMovieAsync(int id) =>
            await _httpClient.DeleteAsync($"Movies/{id}");

        // Repeat similar methods for Directors
        public async Task<List<DirectorDTO>> GetDirectorsAsync() =>
            await _httpClient.GetFromJsonAsync<List<DirectorDTO>>("Directors");

        public async Task<DirectorDTO?> GetDirectorAsync(int id) =>
            await _httpClient.GetFromJsonAsync<DirectorDTO>($"Directors/{id}");

        public async Task<HttpResponseMessage> CreateDirectorAsync(DirectorDTO director) =>
            await _httpClient.PostAsJsonAsync("Directors", director);

    }
}
