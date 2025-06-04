
using MovieStoreB.Infrastructure.Interfaces;
using MovieStoreB.Infrastructure.Services;
using System.Threading.Tasks;
using System.Threading;
using MovieStoreB.Infrastructure;

namespace MovieStoreB.Infrastructure.Services
{
    public class CacheDistributorService : ICacheDistributorService
    {
        private readonly FakeDatabaseService _dbService;

        public CacheDistributorService(FakeDatabaseService dbService)
        {
            _dbService = dbService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var movies = await _dbService.GetAllMoviesAsync();

                foreach (var movie in movies)
                {
                    KafkaMockChannel.CacheTopic.Add(movie);
                }

                await Task.Delay(TimeSpan.FromSeconds(10), cancellationToken);
            }
        }
    }
}
