
using MovieStoreB.BL.Interfaces;
using MovieStoreB.Models.DTO;
using System.Net.Http;
using System.Net.Http.Json;

namespace MovieStoreB.BL.Services
{
    public class ExternalMovieApiService : IExternalMovieApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IMovieService _movieService;

        public ExternalMovieApiService(HttpClient httpClient, IMovieService movieService)
        {
            _httpClient = httpClient;
            _movieService = movieService;
        }

        public async Task FetchAndSaveExternalMoviesAsync()
        {
            var movies = await _httpClient.GetFromJsonAsync<List<Movie>>("http://localhost:5001/external/movies");

            if (movies == null) return;

            foreach (var movie in movies)
            {
                await _movieService.AddMovie(movie);
            }
        }
    }
}
