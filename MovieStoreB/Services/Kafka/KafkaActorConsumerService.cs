using System.Text.Json;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MovieStoreB.Models.DTO;
using MovieStoreB.Services.Memory;

namespace MovieStoreB.Services.Kafka
{
    public class KafkaActorConsumerService : BackgroundService
    {
        private readonly ILogger<KafkaActorConsumerService> _logger;
        private readonly IConsumer<Null, string> _consumer;
        private const string Topic = "actor-cache";

        public KafkaActorConsumerService(ILogger<KafkaActorConsumerService> logger, ConsumerConfig config)
        {
            _logger = logger;
            _consumer = new ConsumerBuilder<Null, string>(config).Build();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("KafkaActorConsumerService started.");
            _consumer.Subscribe(Topic);

            return Task.Run(() =>
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        var cr = _consumer.Consume(stoppingToken);
                        var actor = JsonSerializer.Deserialize<Actor>(cr.Message.Value);

                        if (actor != null)
                        {
                            MemoryActorStorage.AddOrUpdate(actor);
                            _logger.LogInformation("Actor added to memory cache: {Name}", actor.FirstName + " " + actor.LastName);
                        }
                    }
                    catch (ConsumeException ex)
                    {
                        _logger.LogError(ex, "Kafka consume error (actor)");
                    }
                    catch (OperationCanceledException)
                    {
                        break;
                    }
                }

                _consumer.Close();
                _logger.LogInformation("KafkaActorConsumerService stopped.");
            }, stoppingToken);
        }
    }
}
