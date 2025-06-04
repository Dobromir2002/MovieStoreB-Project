
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using MovieStoreB.Infrastructure;
using MovieStoreB.Infrastructure.Interfaces;
using MovieStoreB.Models.DTO;

namespace MovieStoreB.Infrastructure.Services
{
    public class CacheConsumerService : ICacheConsumerService
    {
        private readonly ConcurrentDictionary<string, Movie> _cache = new();

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                foreach (var movie in KafkaMockChannel.CacheTopic.GetConsumingEnumerable(cancellationToken))
                {
                    _cache[movie.Id] = movie;
                }
            }, cancellationToken);
        }

        public List<Movie> GetAllCachedMovies()
        {
            return _cache.Values.ToList();
        }
    }
}
