using System.Text.Json;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MovieStoreB.DL.Interfaces;
using MovieStoreB.Models.DTO;

namespace MovieStoreB.Services.Kafka
{
    public class KafkaCachePublisherService : BackgroundService
    {
        private readonly ILogger<KafkaCachePublisherService> _logger;
        private readonly IMovieRepository _movieRepository;
        private readonly IProducer<Null, string> _producer;
        private const string Topic = "movie-cache";

        public KafkaCachePublisherService(
            ILogger<KafkaCachePublisherService> logger,
            IMovieRepository movieRepository,
            IProducer<Null, string> producer)
        {
            _logger = logger;
            _movieRepository = movieRepository;
            _producer = producer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("KafkaCachePublisherService started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var movies = await _movieRepository.GetMovies();

                    foreach (var movie in movies)
                    {
                        var message = JsonSerializer.Serialize(movie);

                        await _producer.ProduceAsync(Topic, new Message<Null, string>
                        {
                            Value = message
                        }, stoppingToken);

                        _logger.LogInformation("Published movie to Kafka: {Title}", movie.Title);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while publishing movies to Kafka.");
                }

                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }

            _logger.LogInformation("KafkaCachePublisherService stopped.");
        }
    }
}
