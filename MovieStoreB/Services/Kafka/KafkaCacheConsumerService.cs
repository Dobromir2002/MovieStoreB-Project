using System.Text.Json;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MovieStoreB.Models.DTO;
using MovieStoreB.Services.Memory;

namespace MovieStoreB.Services.Kafka
{
    public class KafkaCacheConsumerService : BackgroundService
    {
        private readonly ILogger<KafkaCacheConsumerService> _logger;
        private readonly IConsumer<Null, string> _consumer;
        private const string Topic = "movie-cache";

        public KafkaCacheConsumerService(ILogger<KafkaCacheConsumerService> logger, ConsumerConfig config)
        {
            _logger = logger;
            _consumer = new ConsumerBuilder<Null, string>(config).Build();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("KafkaCacheConsumerService started.");

            _consumer.Subscribe(Topic);

            return Task.Run(() =>
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        var cr = _consumer.Consume(stoppingToken);
                        var movie = JsonSerializer.Deserialize<Movie>(cr.Message.Value);

                        if (movie != null)
                        {
                            MemoryMovieStorage.AddOrUpdate(movie);
                            _logger.LogInformation("Movie added to memory cache: {Title}", movie.Title);
                        }
                    }
                    catch (ConsumeException ex)
                    {
                        _logger.LogError(ex, "Kafka consume error");
                    }
                    catch (OperationCanceledException)
                    {
                        break;
                    }
                }

                _consumer.Close();
                _logger.LogInformation("KafkaCacheConsumerService stopped.");

            }, stoppingToken);
        }
    }
}
