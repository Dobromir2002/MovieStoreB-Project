using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MovieStoreB.DL.Interfaces;
using MovieStoreB.Models.Configurations;
using MovieStoreB.Models.DTO;

namespace MovieStoreB.DL.Repositories.MongoRepositories
{
    public class MoviesRepository : IMovieRepository
    {
        private readonly IMongoCollection<Movie> _movieCollection;

        public MoviesRepository(IOptionsMonitor<MongoDbConfiguration> mongoConfig)
        {
            var client = new MongoClient(mongoConfig.CurrentValue.ConnectionString);
            var db = client.GetDatabase(mongoConfig.CurrentValue.DatabaseName);
            _movieCollection = db.GetCollection<Movie>("Movies");
        }

        public async Task<IEnumerable<Movie>> GetMovies()
        {
            var result = await _movieCollection.FindAsync(_ => true);
            return result.ToList();
        }

        public async Task<Movie?> GetMovieById(string id)
        {
            var result = await _movieCollection.FindAsync(m => m.Id == id);
            return await result.FirstOrDefaultAsync();
        }

        public async Task AddMovie(Movie movie)
        {
            movie.Id = Guid.NewGuid().ToString();
            await _movieCollection.InsertOneAsync(movie);
        }

        public async Task DeleteMovie(string id)
        {
            await _movieCollection.DeleteOneAsync(m => m.Id == id);
        }
    }
}
