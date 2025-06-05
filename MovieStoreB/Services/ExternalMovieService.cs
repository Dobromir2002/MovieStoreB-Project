using System.Text.Json;
using Confluent.Kafka;
using MovieStoreB.DL.Interfaces;
using MovieStoreB.Models.DTO;

namespace MovieStoreB.Services.External
{
    public class ExternalMovieService
    {
        private readonly HttpClient _httpClient;
        private readonly IMovieRepository _movieRepository;
        private readonly IProducer<Null, string> _kafkaProducer;

        public ExternalMovieService(HttpClient httpClient, IMovieRepository movieRepository, IProducer<Null, string> kafkaProducer)
        {
            _httpClient = httpClient;
            _movieRepository = movieRepository;
            _kafkaProducer = kafkaProducer;
        }

        public async Task<Movie?> FetchAndAddMovieAsync()
        {
            var response = await _httpClient.GetAsync("https://dummyjson.com/products/1"); // заменимо с твое API

            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();

            // Фалшиво конвертиране – реално замени с твоя DTO
            var movie = new Movie
            {
                Id = Guid.NewGuid().ToString(),
                Title = $"Imported {DateTime.Now:HH:mm:ss}",
                Genre = "Sci-Fi",
                Description = "Auto-imported",
                Rating = 7.9
            };

            await _movieRepository.AddMovie(movie);

            var jsonMovie = JsonSerializer.Serialize(movie);
            await _kafkaProducer.ProduceAsync("movie-cache", new Message<Null, string> { Value = jsonMovie });

            return movie;
        }
    }
}
