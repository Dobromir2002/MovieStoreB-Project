using System.Text.Json;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MovieStoreB.DL.Interfaces;
using MovieStoreB.Models.DTO;

namespace MovieStoreB.Services.Kafka
{
    public class KafkaActorPublisherService : BackgroundService
    {
        private readonly ILogger<KafkaActorPublisherService> _logger;
        private readonly IActorRepository _actorRepository;
        private readonly IProducer<Null, string> _producer;
        private const string Topic = "actor-cache";

        public KafkaActorPublisherService(
            ILogger<KafkaActorPublisherService> logger,
            IActorRepository actorRepository,
            IProducer<Null, string> producer)
        {
            _logger = logger;
            _actorRepository = actorRepository;
            _producer = producer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("KafkaActorPublisherService started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var actors = await _actorRepository.GetAllAsync();

                    foreach (var actor in actors)
                    {
                        var message = JsonSerializer.Serialize(actor);

                        await _producer.ProduceAsync(Topic, new Message<Null, string>
                        {
                            Value = message
                        }, stoppingToken);

                        _logger.LogInformation("Published actor to Kafka: {FullName}", actor.FirstName + " " + actor.LastName);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while publishing actors to Kafka.");
                }

                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }

            _logger.LogInformation("KafkaActorPublisherService stopped.");
        }
    }
}
