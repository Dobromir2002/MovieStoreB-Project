using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MovieStoreB.DL.Interfaces;
using MovieStoreB.Models.Configurations;
using MovieStoreB.Models.DTO;

namespace MovieStoreB.DL.Repositories.MongoRepositories
{
    internal class MoviesRepository : IMovieRepository
    {
        private readonly IMongoCollection<Movie> _moviesCollection;
        private readonly ILogger<MoviesRepository> _logger;

        public MoviesRepository(ILogger<MoviesRepository> logger, IOptionsMonitor<MongoDbConfiguration> mongoConfig)
        {
            _logger = logger;

            if (string.IsNullOrEmpty(mongoConfig?.CurrentValue?.ConnectionString) || string.IsNullOrEmpty(mongoConfig?.CurrentValue?.DatabaseName))
            {
                _logger.LogError("MongoDb configuration is missing");

                throw new ArgumentNullException("MongoDb configuration is missing");
            }

            var client = new MongoClient(mongoConfig.CurrentValue.ConnectionString);
            var database = client.GetDatabase(mongoConfig.CurrentValue.DatabaseName);

            _moviesCollection = database.GetCollection<Movie>($"{nameof(Movie)}s");
        }

        public async Task AddMovie(Movie movie)
        {
            try
            {
                movie.Id = Guid.NewGuid().ToString();

                await _moviesCollection.InsertOneAsync(movie);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public async Task DeleteMovie(string id)
        {
            await _moviesCollection.DeleteOneAsync(m => m.Id == id);
        }

        public async Task<List<Movie>> GetMovies()
        {
            var result =  await _moviesCollection.FindAsync(m => true);

            return result.ToList();
        }

        public async Task<Movie?> GetMoviesById(string id)
        {
           var result =  await _moviesCollection.FindAsync(m => m.Id == id);

           return result.FirstOrDefault();
        }
    }
}
